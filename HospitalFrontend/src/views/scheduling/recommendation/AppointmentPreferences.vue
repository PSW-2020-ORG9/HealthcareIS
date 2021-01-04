<template>
    <fieldset>
        <legend></legend>
        <h2 class="fs-title">Appointment preferences</h2>
        <select name="Preference" v-model="preference">
            <option value="">Choose your preference..</option>
            <option value="Doctor">Doctor</option>
            <option value="Time">Date</option>
        </select>

        <select name="specialties" v-on:change="chooseSpecialty" v-if="specialtiesFetched">
            <option value="">Choose specialty...</option>
            <option v-bind:value="specialty.id" v-for="specialty in availableSpecialties" v-bind:key="specialty.id">
                {{specialty.name}}
             </option>
        </select>
        <select name="doctor" v-on:change="chooseDoctor" v-if="doctorsFetched">
            <option value="">Your preffered doctor...</option>
            <option v-bind:value="doctor.doctorId" v-for="doctor in availableDoctors" v-bind:key="doctor.doctorId">
                {{"Doctor " + doctor.name + " " + doctor.surname}}
            </option>
        </select>
        <DatePicker :min-date="new Date()" v-model="range" is-range>
            <template v-slot="{ inputValue, inputEvents }">
                <div class="flex justify-left ">
                <input style="width:30%"
                    :value="inputValue.start"
                    v-on="inputEvents.start"
                    class="border px-2 py-1 w-32 rounded focus:outline-none focus:border-indigo-300"
                />
                <svg style="width:10%" 
                    class="w-4 h-4 mx-2"
                    fill="none"
                    viewBox="0 0 24 24"
                    stroke="currentColor"
                >
                    <path
                    stroke-linecap="round"
                    stroke-linejoin="round"
                    stroke-width="2"
                    d="M14 5l7 7m0 0l-7 7m7-7H3"
                    />
                </svg>
                <input style="width:30%"
                    :value="inputValue.end"
                    v-on="inputEvents.end"
                    class="border px-2 py-1 w-32 rounded focus:outline-none focus:border-indigo-300"
                />
                </div>
            </template>
        </DatePicker>
        <input type="button" name="next" class="next action-button"  style="width:50%;" value="Recommend me an appointment!"
          @click="recommendAppointments" v-if="this.range.start != null && this.range.end != null
          && this.preference !='' && this.doctorId!=null && this.specialtyId != null" />
    </fieldset>
</template>

<script>
import api from '../../../constant/api.js'
import axios from 'axios'
import moment from 'moment'
import Toastify from 'toastify-js'
import {DatePicker} from 'v-calendar'

export default {
    name:'AppointmentPreferences',
    data:function(){
        return{
            availableDoctors:[],
            specialtiesFetched:false,
            doctorsFetched:false,
            preference:'',
            availableSpecialties:[],
            doctorId:null,
            specialtyId:null,
            mindate:moment().add(1, 'days').toDate(),
            range:{
                start: moment().add(2, 'days').toDate(),
                end:moment().add(3, 'days').toDate()
            }
        }
    }
    ,
    components:{DatePicker}
    ,
    methods: {
        getAllSpecialties:function(){
            axios.get(api.specialty).then(response=>{
                this.availableSpecialties = response.data
                this.specialtiesFetched = true
            })
        }
        ,
        
        getMinDate:function(){       
            return moment().add(1, 'days').toDate()
        }
        ,
        toastSuccess:function(succesMsg){
            Toastify({
                text: succesMsg,
                duration: '2000',
                newWindow: true,
                close: true,
                gravity: 'top',
                position: 'center',
                backgroundColor: "linear-gradient(to right, #00b09b, #7ecc92)"
                }).showToast()
        },
        chooseSpecialty: function (e) {
            
            this.specialtyId = e.target.value
            this.getDoctorBySpecialty(this.specialtyId)
        }
        ,
        getDoctorBySpecialty:function(id){
            axios.get(api.doctorBySpecialtyUrl(id)).then(response=>{
                this.availableDoctors = response.data
                this.doctorsFetched = true
            })
        }
        ,
        chooseDoctor: function (e) {
            
            this.doctorId = e.target.value
        }
        ,
        recommendAppointments:function(){
            let recommendationRequestDto = {
                DoctorId:this.doctorId,
                SpecialtyId:this.specialtyId,
                TimeInterval:{
                    Start:this.range.start,
                    End:this.range.end
                },
                Preference:this.preference
            }
            axios.post(api.examinationRecommendationUrl,recommendationRequestDto).then(response=>{
                this.$store.commit('setRecommendationDto',response.data)
                this.$router.push("/recommend-appointment/choose-recommended")
            })


            

        }
    },
    mounted:function(){
        this.getAllSpecialties()
    }
}
</script>

<style>
    @import '../../../styles/multipart-form-style.css';
</style>