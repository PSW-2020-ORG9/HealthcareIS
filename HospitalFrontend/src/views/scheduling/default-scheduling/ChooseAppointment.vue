<template>
   <fieldset>
        <h2 class="fs-title">Appointment selection</h2>
        <h3 class="fs-subtitle">Select an appointment which suits you best</h3>

        <h3 v-if="availableAppointments.length == 0">Sorry, there are no available appointments for selected doctor and selected date :(</h3>
        <select v-model="selectedAppointment" v-else name="appointments">
            <option value=''>Select an appointment...</option>
            <option v-bind:value="appointment" v-for="(appointment,index) in availableAppointments" v-bind:key="index">{{this.formAppointmentString(appointment)}}</option>
        </select>
        <input type="button" name="previous" class="previous action-button" value="Previous" @click="goToPrevPage()" />
        <input v-if="selectedAppointment != ''" type="button" name="next" class="next action-button" value="Schedule" @click="scheduleAppointment()"/>
    </fieldset>
</template>

<script>
import api from '../../../constant/api.js'
import axios from 'axios'
import moment from 'moment'
import Toastify from 'toastify-js'

export default {
    name:"ChooseAppointment",
    data:function(){
        return{
            availableAppointments:[],
            selectedAppointment: ''
        }
    }
    ,
    beforeRouteEnter (to, from, next) {
        next(vm=>{
            vm.availableAppointments = vm.$store.state.availableAppointments
        })
    }
    ,
    methods: {
        
        goToPrevPage:function(){
            this.$router.push('/schedule-appointment/choose-doctor')
        }
        ,
        formAppointmentString:function(appointment){
            let appointmentStart=moment(appointment.start)
            let appointmentEnd=moment(appointment.end)
            return appointmentStart.format("dddd, MMMM Do YYYY") +
             " ["+appointmentStart.format("h:mm")+" - "+appointmentEnd.format("h:mm")+"]"
        }
        ,
        selectAppointment: function (e) {
            this.selectedAppointment = e.target.value
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
        scheduleAppointment:function(){
            let appointmentDto=this.$store.state.scheduledExaminationDTO
            appointmentDto['StartTime']=this.selectedAppointment.start
            let selectedAppointment=this.selectedAppointment

            axios.post(api.examination,appointmentDto).then(response=>{
                this.toastSuccess('Appointment on '+this.formAppointmentString(selectedAppointment)+' succesfully scheduled!')
                this.$store.commit('clearAppointmentInfo')
                this.$router.push('/scheduling-type')
         })
        }
    },
}
</script>

<style>
     @import '../../../styles/multipart-form-style.css';
</style>