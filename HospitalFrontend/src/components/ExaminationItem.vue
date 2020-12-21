<template>
    <div class="bg-light row rounded border border-info mt-1">
        <table class="table col-9 h-100">
            <caption></caption>
            <thead>
                <tr class="table-info">
                    <th scope="col">
                        Start time
                    </th>
                    <th scope="col">
                        Room
                    </th>
                    <th scope="col">
                        Doctor
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td class="align-middle" rowspan="2">
                        <span>{{getDate(examination.timeInterval.start)}}</span><br/>
                        <span class="lead">{{getTime(examination.timeInterval.start)}}</span>
                    </td>
                    <td>
                        {{examination.room.name}}
                    </td>
                    <td>
                        {{examination.doctor.person.name + " " + examination.doctor.person.surname}}
                    </td>
                </tr>
                <tr>
                    <td>
                        {{examination.room.department.description}}
                    </td>
                    <td>
                        {{examination.doctor.department.description}}
                    </td>
                </tr>
            </tbody>
        </table>
        <button class="btn btn-info col" v-on:click="cancelAppointment(examination.id)" v-if="isCancelable()">Cancel appointment</button>
        <div class="col text-danger lead font-weight-bold d-flex justify-content-center" v-else-if="examination.isCanceled">
            <h3 class="align-self-center">Canceled</h3>
        </div>
        <button v-else-if="!surveyCompleted && isPassed()" class="btn btn-success col" v-on:click="takeSurvey">Take a survey</button>
        <div v-else-if="surveyCompleted" class="col text-info lead font-weight-bold d-flex justify-content-center">
            <h3 class="align-self-center">Survey completed</h3>
        </div>
        <div v-else class="col text-success lead font-weight-bold d-flex justify-content-center">
            <h3 class="align-self-center">Upcoming</h3>
        </div>
    </div>
</template>

<script>
import Toastify from 'toastify-js'
import axios from 'axios'
import api from '../constant/api.js'

export default {
    name: "ExaminationItem",
    props: {
        examination: Object,
        surveyCompleted: Boolean
    },
    methods: {
        getDate: function(date) {
            let dateObj = new Date(date)
            return dateObj.getDate() + "." + (dateObj.getMonth() + 1) + "." + dateObj.getFullYear() + "."
        },
        getTime: function(date) {
            let dateObj = new Date(date)
            let minutes = dateObj.getMinutes()
            return dateObj.getHours() + ":" + ((minutes < 10) ? ("0" + minutes) : minutes)
        },
        isCancelable: function () {
            if (this.examination.isCanceled) return false
            let dateObj = new Date(this.examination.timeInterval.start)
            let next48hours = new Date()
            next48hours.setDate(next48hours.getDate() + 2)
            if(dateObj.getTime() < next48hours.getTime()) return false
            return true
        },
        isPassed: function () {
            if (this.examination.isCanceled) return false
            let dateObj = new Date(this.examination.timeInterval.start)
            if(dateObj < Date.now()) return true
            return false
        },
        cancelAppointment: function (id) {
            axios.get(api.patientExaminations + '/cancel/' + id)
            .then(response => {
                this.toastSuccess()
                this.$emit('update-examinations')
            })
            .catch(err => {
                this.toastError()
            })
        },
        takeSurvey: function () {
            this.$router.push('/survey/' + this.examination.doctor.id + "/" + this.examination.id)
        },
        toastSuccess: function () {
            Toastify({
                text: "Appointment canceled.",
                duration: '2000',
                newWindow: true,
                close: true,
                gravity: 'top',
                position: 'center',
                backgroundColor: "linear-gradient(to right, #00b09b, #7ecc92)"
            }).showToast()
        },
        toastError: function () {
            Toastify({
                text: "Error canceling appointment!",
                duration: '2000',
                newWindow: true,
                close: true,
                gravity: 'top',
                position: 'center',
                backgroundColor: "linear-gradient(to right, #C37D92, #d89a9e)"
            }).showToast()
        },
    },
}
</script>