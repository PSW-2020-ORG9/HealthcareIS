<template>
  <fieldset>
        <h2 class="fs-title">Appointment selection</h2>
       
        <div v-if="recommendedAppointments!=null">
            <h3  class="fs-subtitle">We found you an appointment!</h3>
                <select v-model="selectedAppointment" >
                    <option value=''>Select an appointment...</option>
                    <option v-bind:value="appointment" v-for="(appointment,index) in availableAppointments" v-bind:key="index">
                        {{this.formAppointmentString(appointment)}}
                    </option>
                </select>
        </div>
        <h3 v-else>Sorry, there are no available appointments that match your preferences</h3>
        <input type="button" name="previous" style="width:50%;" class="previous action-button" value="Change your preferences" @click="goToPrevPage()" />
        <input v-if="recommendedAppointment!=null"  type="button" name="next" class="next action-button" value="Schedule" @click="scheduleAppointment()" />
    </fieldset>
</template>

<script>
import moment from 'moment'
import api from '../../../constant/api.js'
import axios from 'axios'
import Toastify from 'toastify-js'
export default {
    name:'ChooseRecommended',
    data:function(){
        return{
            recommendedAppointments:null,
            selectedAppointment: ''
        }
    },
     beforeRouteEnter (to, from, next) {
        next(vm=>{
            vm.recommendedAppointments = vm.$store.state.recommendationDto
        })
    },
    methods: {
        goToPrevPage:function(){
            this.$router.push('/recommend-appointment/')
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
        formAppointmentString:function(appointment){
            
            //TODO: format dropdown list item
            /*
            let appointmentStart=moment(timeInterval.start)
            let appointmentEnd=moment(timeInterval.end)
            
            return appointmentStart.format("dddd, MMMM Do YYYY") +
             " "+appointmentStart.format("h:mm")+" - "+appointmentEnd.format("h:mm")
            */
        },
        selectAppointment: function (e) {
            this.selectedAppointment = e.target.value
        }
        ,
        scheduleAppointment:function(){
            let scheduledExaminationDto = {
                StartTime:this.selectedAppointment.timeInterval.start,
                DoctorId:this.selectedAppointment.doctor.id,
                PatientId:1
            }
            let recommendedAppointment=this.recommendedAppointment
            axios.post(api.examination,scheduledExaminationDto).then(response=>{
                this.toastSuccess('Appointment on '+this.formAppointmentString(recommendedAppointment.timeInterval)+' succesfully scheduled!')
                this.$router.push('/scheduling-type/')
            
            })
        }
    },
}
</script>

<style>

</style>