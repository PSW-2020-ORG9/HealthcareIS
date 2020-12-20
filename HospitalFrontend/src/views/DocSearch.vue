<template>
<link rel="stylesheet" type="text/css" href="https://cdn.jsdelivr.net/npm/toastify-js/src/toastify.min.css">

    <h2 class="mb-5"> Search your own medical documentation</h2>

    <div v-if="simpleQueryOpened">
        <div class="container d-inline-flex mb-4" v-if="toggleSimpleQuery">
            <input type="text" placeholder="Medication name.." class="form-control mr-5 col" @keyup.enter="simpleSearchPrescriptions" v-model="prescriptionNameQuery">
            <div class="col d-inline-flex">
                <input type="text" placeholder="Doctor name.." class="form-control mr-2" @keyup.enter="simpleSearchExaminations" v-model="doctorNameQuery">
                <input type="text" placeholder="Doctor surname.." class="form-control ml-2" @keyup.enter="simpleSearchExaminations" v-model="doctorSurnameQuery">
            </div>
        </div>
        <div class="container mb-5">
          <button @click="toggleAdvancedQuery" class="btn btn-success pr-5 pl-5 pt-2 pb-2">Advanced</button>
        </div>
    </div>
    <div v-else>
        <div class="container d-inline-flex mb-4" v-if="toggleSimpleQuery">
            <div class="col">
                <div class="row container mb-3">
                    <input type="text" placeholder="Medication name.." class="form-control mr-1 col" @keyup.enter="advancedSearchPrescriptions" v-model="prescriptionNameQuery">
                    <input type="text" placeholder="Manufacturer.." class="form-control mr-1 col" @keyup.enter="advancedSearchPrescriptions" v-model="prescriptionManufacturerQuery">
                </div>
                <div class="row container">
                    <input type="text" placeholder="Diagnosis.." class="form-control mr-1 col" @keyup.enter="advancedSearchPrescriptions" v-model="prescriptionDiagnosisQuery">
                    <select v-model="prescriptionStatusQuery">
                        <option>All</option>
                        <option>Ongoing</option>
                        <option>Past</option>
                        <option>Future</option>
                    </select>
                </div>
            </div>
            <div class="col d-inline-flex mb-2">
                <div class="col">
                    <div class="row container mb-3">
                        <input type="text" placeholder="Doctor name.." class="form-control mr-1 col" @keyup.enter="advancedSearchExaminations" v-model="doctorNameQuery">
                        <input type="text" placeholder="Doctor surname.." class="form-control mr-1 col" @keyup.enter="advancedSearchExaminations" v-model="doctorSurnameQuery">
                    </div>
                    <div class="row container mb-3">
                        <input type="text" placeholder="Procedure details.." class="form-control mr-1 col" @keyup.enter="advancedSearchExaminations" v-model="examinationDetailsQuery">
                        <input type="text" placeholder="Doctor specialty.." class="form-control mr-1 col" @keyup.enter="advancedSearchExaminations" v-model="doctorSpecialtyQuery">
                    </div>
                    <div class="row container">
                        <select v-model="examinationStatusQuery" class="w-50">
                            <option>All</option>
                            <option>Future</option>
                            <option>Past</option>
                        </select>
                    </div>
                </div>
            </div>
        </div>
        <div class="container mb-5">
            <button @click="toggleSimpleQuery" class="btn-info pr-5 pl-5 pt-2 pb-2">Simple</button>
        </div>
    </div>

    <div class="container" v-if="rendered">
        <div class="row">
            <div class="col">
                <table class="table">
                    <caption></caption>
                    <tbody>
                    <tr v-for="(prescription, index) in prescriptions" :key="index">
                        <td class="row mb-2" style="background-color: rgb(238, 238, 238); padding-left: 25px; margin-right: 10px;">
                            <div class="col">
                                <div class="row font-weight-bold">
                                    {{prescription.medication.name}}
                                </div>
                                <div class="container mt-3">
                                    <div class="row mb-2">
                                        <span class="font-weight-bold"> Times per day: </span>
                                        {{prescription.instructions.timesPerDay}}
                                    </div>
                                    <div class="row mb-2">
                                        <span class="font-weight-bold"> Dosage: </span>
                                        {{prescription.instructions.dosage}}
                                        {{prescription.instructions.dosageUnit}}
                                    </div>
                                    <div class="row mb-2">
                                        <span class="font-weight-bold"> Diagnosis: </span>
                                        {{prescription.diagnosis.name}}
                                    </div>
                                    <div class="row mb-2">
                                        <span class="font-weight-bold"> Start date: </span>
                                        {{new Date(prescription.instructions.startDate).toLocaleDateString()}}
                                    </div>
                                    <div class="row mb-2">
                                        <span class="font-weight-bold"> End date: </span>
                                        {{new Date(prescription.instructions.endDate).toLocaleDateString()}}
                                    </div>
                                    <div class="row mb-2 font-italic">
                                        {{prescription.instructions.description}}
                                    </div>
                                </div>
                            </div>
                        </td>
                    </tr>
                    </tbody>
                </table>
            </div>

            <div class="col">
                <table class="table">
                    <caption></caption>
                    <tbody>
                    <tr v-for="(exam, index) in examinations" :key="index">
                        <td class="row mb-2" style="background-color: rgb(238, 238, 238); padding-left: 25px; margin-right: 10px;">
                            <div class="col">
                                <div class="row font-weight-bold">
                                    {{new Date(exam.timeInterval.start).toLocaleDateString()}}
                                </div>
                                <div class="row mt-2">
                                    Doctor: {{exam.doctor.person.name}} {{exam.doctor.person.surname}}
                                </div>
                              <div class="row mt-2">
                                Description: {{exam.procedureDetails.description}}
                              </div>
                                <div class="row mt-2">
                                    Anamnesis: {{exam.examinationReport.anamnesis}}
                                </div>
                                <div class="row mt-2">
                                    Diagnoses:
                                </div>
                                <div class="container">
                                    <div class="row mb-2">
                                        <table class="table">
                                            <caption></caption>
                                            <tr v-for="(diagnosis, index) in exam.examinationReport.diagnoses" :key="index">
                                                <td class="mb-2">
                                                    <div class="container text-left">
                                                        <div>
                                                            {{diagnosis.name}} - {{diagnosis.description}}
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                                <div class="row mt-2">
                                    Medications:
                                </div>
                                <div class="container">
                                    <div class="row">
                                        <table class="table">
                                            <caption></caption>
                                            <tr v-for="(prescription, index) in exam.examinationReport.prescriptions" :key="index">
                                                <td class="mb-2">
                                                    <div class="container text-left">
                                                        <div>
                                                            {{prescription.medication.name}}
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </td>
                    </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>

</template>

<script>
import axios from 'axios';
import api from '../constant/api.js'
import Toastify from 'toastify-js'

export default {
    name:'DocSearch',
    data: function()
    {
        return {
            prescriptions: [],
            examinations: [],

            doctorNameQuery: "",
            doctorSurnameQuery: "",

            examinationStatusQuery : "All",
            examinationDetailsQuery : "",
            doctorSpecialtyQuery : "",

            prescriptionNameQuery: "",

            prescriptionStatusQuery: "All",
            prescriptionManufacturerQuery: "",
            prescriptionDiagnosisQuery: "",

            rendered: false,
            simpleQueryOpened: true
        }
    },
    methods: {
        simpleSearchPrescriptions() {
            this.prescriptions = [];
            if (!this.prescriptionNameQuery) {
                this.getAllPrescriptions();
                return;
            }
            axios.get(api.docSearchPrescriptionSimple
                + "?" + "medicationName=" + this.prescriptionNameQuery
            )
            .then(response => this.handlePrescriptions(response))
            .catch(() => {this.failedConnection()})
        },
        simpleSearchExaminations() {
            this.examinations = [];
            if (!this.doctorNameQuery && !this.doctorSurnameQuery) {
                this.getAllExaminations();
                return;
            }
            axios.get(api.docSearchExaminationSimple
                + "?" + "name=" + this.doctorNameQuery + "&"
                + "surname=" + this.doctorSurnameQuery
            )
            .then(response => this.handleExaminations(response))
            .catch(() => {this.failedConnection()})
        },
        advancedSearchPrescriptions() {
            this.prescriptions = [];
            axios.post(api.docSearchPrescriptionAdvanced, {
              Manufacturer: this.prescriptionManufacturerQuery,
              Name: this.prescriptionNameQuery,
              Diagnosis: this.prescriptionDiagnosisQuery,
              Status: this.prescriptionStatusQuery
            })
            .then(response => this.handlePrescriptions(response))
            .catch(() => {this.failedConnection()})
        },
        advancedSearchExaminations() {
          this.examinations = [];
          axios.post(api.docSearchExaminationAdvanced, {
            Status: this.examinationStatusQuery,
            DoctorName: this.doctorNameQuery,
            DoctorSurname: this.doctorSurnameQuery,
            ProcedureDetails: this.examinationDetailsQuery,
            DoctorSpecialty: this.doctorSpecialtyQuery
          })
          .then(response => this.handleExaminations(response))
          .catch(() => this.toastError());
        },
        getAllExaminations() {
            axios.get(api.examinations)
            .then(response => this.handleExaminations(response))
        },
        getAllPrescriptions() {
            axios.get(api.prescriptions)
                .then(response => this.handlePrescriptions(response));
        },
        toastError(message) {
            Toastify({
              text: message,
              duration: '2000',
              newWindow: true,
              close: true,
              gravity: 'top',
              position: 'center',
              backgroundColor: "linear-gradient(to right, #C37D92, #d89a9e)"
            }).showToast()
        },
        handleExaminations(response) {
            if (response.data && response.data.length) {
              response.data.forEach(exam => {
                this.examinations.push(exam);
              })
            }
            else {
                this.toastError("No examinations found.");
            }
        },
        handlePrescriptions(response) {
            if (response.data && response.data.length) {
                response.data.forEach(prescription => {
                    this.prescriptions.push(prescription);
                });
            }
            else {
                this.toastError("No prescriptions found.");
            }
        },
        failedConnection() {
            this.toastError("Failed to connect.");
        },
        toggleAdvancedQuery() {
            this.simpleQueryOpened = false;
        },
        toggleSimpleQuery() {
            this.simpleQueryOpened = true;
        }
    },
    mounted() {
        this.rendered = true;
        this.getAllExaminations();
        this.getAllPrescriptions();
    }
}
</script>

<style>

</style>