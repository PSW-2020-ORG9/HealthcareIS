<template>
    <div>
        <h1>Your appointments</h1>
        <div class="container w-50">
            <div name="passed" class="container bg-light border border-gray">
                <div class="text-left">Past appointments</div>
                <ExaminationItem v-for="examination in getPastAppointments()" v-bind:key="examination.id" v-bind:examination="examination"></ExaminationItem>
            </div>
            <div class="container bg-light border border-gray" name="today">
                <div class="text-left">Today's appointments</div>
                <ExaminationItem v-for="examination in getTodaysAppointments()" v-bind:key="examination.id" v-bind:examination="examination"></ExaminationItem>
            </div>
            <div class="container bg-light border border-gray" name="upcoming">
                <div class="text-left">Future appointments</div>
                <ExaminationItem v-for="examination in getFutureAppointments()" v-bind:key="examination.id" v-bind:examination="examination"></ExaminationItem>
            </div>
        </div>
    </div>
</template>

<script>
import axios from 'axios'
import api from '../constant/api.js'
import ExaminationItem from '../components/ExaminationItem.vue'

export default {
    name: "ObservePatientExaminations",
    data: function () {
        return {
            examinations: []
        }
    },
    components: {
        ExaminationItem
    },
    mounted: function () {
        axios.get(api.patientExaminations + '/patient/1').then(response => {
            this.examinations = response.data
            console.log(response.data)
        })
    },
    methods: {
        getPastAppointments: function () {
            let pastAppointments = []
            let now = new Date()
            this.examinations.forEach(e => {
                let dateObj = new Date (e.timeInterval.start)
                if (dateObj.getDate() < now.getDate() && dateObj.getMonth() <= now.getMonth() && dateObj.getFullYear() <= now.getFullYear()) {
                    pastAppointments.push(e)
                }
            })
            return pastAppointments
        },
        getTodaysAppointments: function () {
            let todaysAppointments = []
            let now = new Date()
            this.examinations.forEach(e => {
                let dateObj = new Date (e.timeInterval.start)
                if (dateObj.getDate() == now.getDate() && dateObj.getMonth() == now.getMonth() && dateObj.getFullYear() == now.getFullYear()) {
                    todaysAppointments.push(e)
                }
            })
            return todaysAppointments
        },
        getFutureAppointments: function () {
            let futureAppointments = []
            let now = new Date()
            this.examinations.forEach(e => {
                let dateObj = new Date (e.timeInterval.start)
                if (dateObj.getDate() > now.getDate() && dateObj.getMonth() >= now.getMonth() && dateObj.getFullYear() >= now.getFullYear()) {
                    futureAppointments.push(e)
                }
            })
            return futureAppointments
        }
    
    }
}
</script>

<style>

</style>