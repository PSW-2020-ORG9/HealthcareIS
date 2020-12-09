<template>
    <fieldset>
        <h2 class="fs-title">Appointment preferences</h2>
        <select name="Preference" v-model="preference">
            <option value="">Choose your preference..</option>
            <option value="Doctor">Doctor</option>
            <option value="Period">Time period</option>
        </select>

        <select name="departments" v-if="departmentsFetched">
            <option value="">Choose department...</option>
            <option v-bind:value="index" v-for="(department,index) in availableDepartments"
            @click="chooseDepartment(department.id)" v-bind:key="index">
                {{department.name}}
             </option>
        </select>
        <select name="doctor" v-if="doctorsFetched">
            <option value="">Your preffered doctor...</option>
            <option v-bind:value="index" v-for="(doctor,index) in availableDoctors"
            @click="chooseDoctor(doctor.doctorId)" v-bind:key="index">
                {{"Doktor " + doctor.name + " " + doctor.surname}}
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
          && this.preference !='' && this.doctorId!=null && this.departmentId != null" />
    </fieldset>
</template>

<script>
import api from '../../../constant/api.js'
import axios from 'axios'
import moment from 'moment'
import Toastify from 'toastify-js'
import Datepicker from 'vue3-datepicker'
import {Calendar, DatePicker} from 'v-calendar'

export default {
    name:'AppointmentPreferences',
    data:function(){
        return{
            availableDoctors:[],
            departmentsFetched:false,
            doctorsFetched:false,
            preference:'',
            availableDepartments:[],
            doctorId:null,
            departmentId:null,
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
        getAllDepartments:function(){
            axios.get(api.department).then(response=>{
                this.availableDepartments = response.data
                this.departmentsFetched = true
            })
        }
        ,
        getMinDate:function(){       
            return moment().add(1, 'days').toDate()
        }
        ,
        chooseDepartment:function(id){
            this.departmentId=id
            this.getDoctorByDepartment(id)
        }
        ,
        getDoctorByDepartment:function(id){
            axios.get(api.doctorByDepartmentUrl(id)).then(response=>{
                this.availableDoctors = response.data
                this.doctorsFetched = true
            })
        }
        ,
        chooseDoctor:function(doctorId){
            this.doctorId=doctorId
        }
        ,
        recommendAppointments:function(){
            //TODO: fetch all appointments and commit them
            this.$router.push("/recommend-appointment/choose-recommended")

        }
    },
    mounted:function(){
        this.getAllDepartments()
    }
}
</script>

<style>
    @import '../../../styles/multipart-form-style.css';
</style>