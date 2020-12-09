<template>
   <fieldset>
        <h2 class="fs-title">Doctor selection</h2>
        <h3 class="fs-subtitle">Choose your favourite doctor!</h3>
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
        <input type="button" name="previous" class="previous action-button" value="Previous" @click="goToPrevPage()" />
        <input type="button" name="next" class="next action-button" value="Next" @click="getAvailableAppointments" />
    </fieldset>
</template>

<script>
import api from '../../../constant/api.js'
import axios from 'axios'
import Toastify from 'toastify-js'
import moment from 'moment'

export default {
    name:"ChooseDoctor",
    data:function(){
        return{
            availableDoctors:[],
            departmentsFetched:false,
            doctorsFetched:false,
            availableDepartments:[],
            doctorId:null,
            departmentId:null,
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
            this.$router.push('/schedule-appointment/choose-appointment')
        },
        goToPrevPage:function(){
            this.$router.push('/schedule-appointment/')
        },
        chooseDoctor:function(doctorId){
            this.doctorId=doctorId
            this.$store.commit('setDoctorId', doctorId)
            
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
            if(this.doctorId == null || this.departmentId == null){
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
        getAllDepartments:function(){
            axios.get(api.department).then(response=>{
                this.availableDepartments = response.data
                this.departmentsFetched = true
            })
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
        
    }
    ,
    mounted:function(){
        this.getAllDepartments()
    }
    
}
</script>

<style>
     @import '../../../styles/multipart-form-style.css';
</style>