<template>
    <fieldset>
        <legend></legend>
        <h2 class="fs-title">Date selection</h2>
        <h3 class="fs-subtitle">Choose preffered date</h3>
        <Datepicker v-model="selectedDate" :lowerLimit="getMinDate()"></Datepicker>
        <input type="button" name="next" class="next action-button" value="Next" @click="goToNextPage()"/>
    </fieldset>
</template>

<script>
import api from '../../../constant/api.js'
import axios from 'axios'
import Toastify from 'toastify-js'
import moment from 'moment'
import Datepicker from 'vue3-datepicker'
import 'vue3-datepicker/dist/vue3-datepicker.css'

export default {
    name:"ChooseDate",
    data:function(){
        return{
            selectedDate:moment().add(1, 'days').toDate()
        }
    }
    ,
    components:{Datepicker}
    ,
    methods:{
        getAvailableDoctors:function(){
            let date = moment(this.selectedDate).format('YYYY-MM-DD')
            let selectedDate = this.selectedDate
            axios.get(api.availableDoctorUrl(date)).then(response => {
                if(response.data.length == 0){
                    this.toastErrorMessage('There are no available doctors on this day')
                    return;
                }
        
                this.$store.commit('setAvailableDoctors', response.data)
                this.$store.commit('setSelectedDate',selectedDate)
                this.$router.push('/schedule-appointment/choose-doctor')
            })
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
        goToNextPage:function(){
            this.$store.commit('setSelectedDate',this.selectedDate)
            this.$router.push('/schedule-appointment/choose-doctor')
        }
        ,
        getMinDate:function(){       
            return moment().add(1, 'days').toDate()
        }
    },
    mounted() {
        let previoslySelectedDate=this.$store.state.selectedDate
        if(previoslySelectedDate!=null)
            this.selectedDate=previoslySelectedDate
    },
    
}
</script>

<style>
     @import '../../../styles/multipart-form-style.css';
</style>