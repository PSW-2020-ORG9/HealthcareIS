<template>
   <fieldset>
        <h2 class="fs-title">Doctor selection</h2>
        <h3 class="fs-subtitle">Choose your favourite doctor!</h3>
        <select name="doctor">
            <option value="">Your preffered doctor...</option>
            <option v-bind:value="index" v-for="(doctor,index) in availableDoctors" @click="chooseDoctor(doctor.doctorId)" v-bind:key="index">{{"Doktor " + doctor.name + " " + doctor.surname + " ["+ doctor.departmentName + "]"}}</option>
        </select>
        <input type="button" name="previous" class="previous action-button" value="Previous" @click="goToPrevPage()" />
        <input type="button" name="next" class="next action-button" value="Next" @click="getAvailableAppointments" />
    </fieldset>
</template>

<script>
import api from '../../constant/api.js'
import axios from 'axios'
import Toastify from 'toastify-js'
import moment from 'moment'

export default {
    name:"ChooseDoctor",
    data:function(){
        return{
            availableDoctors:[],
            doctorId:null
        }
    }
    ,
    beforeRouteEnter (to, from, next) {
        next(vm=>{
            vm.availableDoctors = vm.$store.state.availableDoctors
        })
        
    }
    ,
    methods: {
        goToNextPage:function(){
            this.$router.push('/schedule/choose-appointment')
        },
        goToPrevPage:function(){
            this.$router.push('/schedule/')
        },
        chooseDoctor:function(doctorId){
            this.doctorId=doctorId
            this.$store.commit('setDoctorId', doctorId)
            
        }
        ,
        getAvailableAppointments:function(){
            let date =  moment(this.$store.state.selectedDate).format('YYYY-MM-DD')
            axios.get(api.availableIntervalUrl(date,this.doctorId)).then(response => {
                this.$store.commit('setAvailableAppointments', response.data)
                this.$router.push('/schedule/choose-appointment')
            })
        }
        
    }
    
}
</script>

<style>
     @import '../../styles/multipart-form-style.css';
</style>