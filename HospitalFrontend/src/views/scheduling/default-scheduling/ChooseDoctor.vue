<template>
    <fieldset>
        <legend></legend>
        <h2 class="fs-title">Doctor selection</h2>
        <h3 class="fs-subtitle">Choose your favourite doctor!</h3>
        <select v-on:change="chooseSpecialty" name="specialties" v-if="specialtiesFetched">
            <option value="">Choose specialty...</option>
            <option v-bind:value="specialty.id" v-for="specialty in availableSpecialties" v-bind:key="specialty.id">
                {{specialty.name}}
             </option>
        </select>
        <select v-on:change="chooseDoctor" name="doctor" v-if="doctorsFetched">
            <option value="">Your preffered doctor...</option>
            <option v-bind:value="doctor.doctorId" v-for="doctor in availableDoctors" v-bind:key="doctor.doctorId">
                {{"Doctor " + doctor.name + " " + doctor.surname}}
            </option>
        </select>
        <input type="button" name="previous" class="previous action-button" value="Previous" @click="goToPrevPage()" />
        <input type="button" name="next" class="next action-button" value="Next" @click="getAvailableAppointments" />
    </fieldset>
</template>

<script>
import api from '../../../constant/api.js'
import axios from 'axios'
import Toastify from 'toastify-js'
import moment from 'moment'
import { publishSchedulingEvent } from '../../../services/eventPublisher.js'

export default {
    name:"ChooseDoctor",
    data:function(){
        return{
            availableDoctors:[],
            specialtiesFetched:false,
            doctorsFetched:false,
            availableSpecialties:[],
            doctorId:null,
            specialtyId:null,
        }
    }
    ,
    beforeRouteEnter (to, from, next) {
        next(vm=>{
            if(from.name===undefined){
                vm.$router.push('/scheduling-type')
                return
            }
            vm.availableDoctors = vm.$store.state.availableDoctors
            vm.publishEvent()
        })
        
    }
    ,
    methods: {
        goToNextPage:function(){
            this.$router.push('/schedule-appointment/choose-appointment')
        },
        goToPrevPage:function(){
            this.$router.push('/schedule-appointment/')
        },
        chooseDoctor: function (e) {
            this.doctorId = e.target.value
            this.$store.commit('setDoctorId', this.doctorId)
            
        }
        ,
        toastErrorMessage:function(message){
            Toastify({
                    text: message,
                    duration: '2000',
                    newWindow: true,
                    close: true,
                    gravity: 'top',
                    position: 'center',
                    backgroundColor: "linear-gradient(to right, #C37D92, #d89a9e)"
                }).showToast()
        }
        ,
        getAvailableAppointments:function(){
            if(this.doctorId == null || this.specialtyId == null){
                this.toastErrorMessage("Please select preffered doctor.")
                return
            }

            let date =  moment(this.$store.state.selectedDate).format('YYYY-MM-DD')
            axios.get(api.availableIntervalUrl(date,this.doctorId)).then(response => {
                this.$store.commit('setAvailableAppointments', response.data)
                this.$router.push('/schedule-appointment/choose-appointment')
            })
        }
        ,
        getAllSpecialties:function(){
            axios.get(api.specialty).then(response=>{
                this.availableSpecialties = response.data
                this.specialtiesFetched = true
            })
        }
        ,
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
         publishEvent:function(){
			publishSchedulingEvent({
				"eventType" : "STEP_2",
                "userAge" : this.$store.state.user.age,
                "schedulingSessionId": this.$store.state.schedulingSessionUuid
			})
		}
        
    }
    ,
    mounted:function(){
        this.getAllSpecialties()
    }
    
}
</script>

<style>
     @import '../../../styles/multipart-form-style.css';
</style>