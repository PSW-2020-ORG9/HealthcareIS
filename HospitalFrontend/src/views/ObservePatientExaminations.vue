<template>
    <link rel="stylesheet" type="text/css" href="https://cdn.jsdelivr.net/npm/toastify-js/src/toastify.min.css">
    <div class="modal fade" id="modal" role="dialog" aria-labelledby="modal" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Examination Report</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body text-left" v-if="selectedExamination && selectedExamination.examinationReport">
                    <b>Anamnesis:</b> {{selectedExamination.examinationReport.anamnesis}}<br/><hr>
                    <b>Diagnosis:</b> {{selectedExamination.examinationReport.diagnoses[0].name}}<br/><hr>
                    <b>Diagnosis description:</b> {{selectedExamination.examinationReport.diagnoses[0].description}}<br/><hr>
                    <b>Start:</b> {{formatDate(selectedExamination.timeInterval.start)}}<br/><hr>
                    <b>End:</b> {{formatDate(selectedExamination.timeInterval.end)}}<br/><hr>
                    <b>Doctor:</b> Dr. {{selectedExamination.doctor.person.name}} {{selectedExamination.doctor.person.surname}}<br/><hr>
                    <b>Examined in:</b> {{selectedExamination.room.name}}, {{selectedExamination.room.department.description}}
                </div>
                <div v-else>
                    <b>This examination has no additional information.</b>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
    <div>
        <h1>Your appointments</h1>
        <div class="container w-50" v-if="loaded">
            <div name="passed" class="container bg-light border border-gray">
                <div class="text-left text-dark lead">Past appointments</div>
                <ExaminationItem v-for="(examination, index) in getPastAppointments()" style="cursor: pointer;" data-toggle="modal" data-target="#modal" @click="selectedExamination = examination"
                                 v-bind:key="examination.id" v-bind:examination="examination"
                                 v-bind:surveyCompleted="surveyStatuses[index]"></ExaminationItem>
            </div>
            <div class="container bg-light border border-gray" name="today">
                <div class="text-left text-success lead">Today's appointments</div>
                <ExaminationItem v-for="examination in getTodaysAppointments()"
                                 v-bind:key="examination.id" v-bind:examination="examination"
                                 v-bind:surveyCompleted="false"></ExaminationItem>
            </div>
            <div class="container bg-light border border-gray" name="upcoming">
                <div class="text-left text-info lead">Future appointments</div>
                <ExaminationItem v-for="examination in getFutureAppointments()"
                                 v-on:update-examinations="getExaminations()" v-bind:key="examination.id"
                                 v-bind:examination="examination"
                                 v-bind:surveyCompleted="false"
                ></ExaminationItem>
            </div>
        </div>
    </div>
</template>

<script>
import axios from 'axios'
import api from '../constant/api.js'
import ExaminationItem from '../components/ExaminationItem.vue'
import moment from 'moment'

export default {
    name: "ObservePatientExaminations",
    data: function () {
        return {
            examinations: [],
            surveyStatuses: [],
            loaded: false,
            selectedExamination: null
        }
    },
    components: {
        ExaminationItem
    },
    mounted: function () {
        this.getExaminations()
    },
    methods: {
        getExaminations() {
            axios.get(api.patientExaminations)
            .then(response => {
                this.examinations = response.data
                this.sortAppointments(this.examinations)
                this.getSurveyStatuses()
            })
        },
        getSurveyStatuses() {
            let payload = []
            this.examinations.forEach(examination => {
                if (new Date(examination.timeInterval.start) < Date.now()) {
                  payload.push(examination.id)
                }
            })
            axios.post(api.patientExaminationsSurveyResponsesUrl, payload)
            .then(response => {
                if (response.status === 200) {
                    this.surveyStatuses = response.data
                    this.loaded = true
                }
            })
        },
        getPastAppointments: function () {
            let pastAppointments = []
            this.examinations.forEach(e => {
                if (moment(e.timeInterval.start).isBefore(moment())) {
                    pastAppointments.push(e)
                }
            })
            return pastAppointments
        },
        getTodaysAppointments: function () {
            let todaysAppointments = []
            let today = moment().endOf('day')
            this.examinations.forEach(e => {
                let dateObj = moment(e.timeInterval.start)
                if (dateObj.endOf('day').isSame(today) && dateObj.isAfter(moment())) {
                    todaysAppointments.push(e)
                }
            })
            return todaysAppointments
        },
        getFutureAppointments: function () {
            let futureAppointments = []
            let today = moment().endOf('day')
            this.examinations.forEach(e => {
                if (moment(e.timeInterval.start).isAfter(today)) {
                    futureAppointments.push(e)
                }
            })
            return futureAppointments
        },
        sortAppointments: function (appointments) {
            return appointments.sort((a1, a2) => new Date(a1.timeInterval.start) - new Date(a2.timeInterval.start))
        },
        formatDate: function (date) {
            return moment(date).format("hh:mm D.M.YYYY.")
        }
    }
}
</script>