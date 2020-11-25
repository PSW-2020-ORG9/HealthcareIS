<template>
<link rel="stylesheet" type="text/css" href="https://cdn.jsdelivr.net/npm/toastify-js/src/toastify.min.css">
  
    <h2 class="mb-5"> Search your own medical documentation</h2>

    <div class="container d-inline-flex mb-5">
        <input type="text" placeholder="Medication name.." class="form-control mr-5 col" @keyup.enter="searchPrescriptions" v-model="prescriptionsQuery">
        <div class="col d-inline-flex">
            <input type="text" placeholder="Doctor name.." class="form-control mr-2" @keyup.enter="searchExaminations" v-model="doctorNameQuery">
            <input type="text" placeholder="Doctor surname.." class="form-control ml-2" @keyup.enter="searchExaminations" v-model="doctorSurnameQuery">
        </div>
    </div>

    <div class="container" v-if="rendered">
        <div class="row">
            <div class="col">
                <table class="table">
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
                                    Anamnesis: {{exam.examinationReport.anamnesis}} 
                                </div>
                                <div class="row mt-2"> 
                                    Diagnoses:
                                </div>
                                <div class="container">
                                    <div class="row mb-2">
                                        <table class="table"> 
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
        prescriptionsQuery: "",

        rendered: false
        }
    },
    methods: {
        searchPrescriptions() {
            if (!this.prescriptionsQuery) {
                return;
            }
            this.prescriptions = [];
            axios.get(api.docSearchPrescriptionSimple + this.prescriptionsQuery)
                .then(response => {
                    if (response.data && response.data.length) {
                        response.data.forEach(prescription => {
                            console.log(prescription)
                            this.prescriptions.push(prescription);
                        });
                    }
                    else {
                        this.toastError("No prescriptions for " + "'" + this.prescriptionsQuery + "'");
                    }
                })
                .catch (error => {
                    this.failedConnection();
                })
        },
        searchExaminations() {
            if (!this.doctorNameQuery && !this.doctorSurnameQuery) {
                return;
            }
            this.examinations = []
            axios.post(api.docSearchExaminationSimple, {
                name : this.doctorNameQuery,
                surname : this.doctorSurnameQuery
            })
            .then(response => {
                if (response.data && response.data.length) {
                    response.data.forEach(exam => {
                        this.examinations.push(exam);
                    })
                }
                else {
                    this.toastError("No examinations found for doctor " + "'" + this.doctorNameQuery + " " + this.doctorSurnameQuery + "'");
                }
            })
            .catch(error => {
                this.failedConnection();
            })
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
        failedConnection() {
            this.toastError("Failed to connect.");
        }
    },
    mounted() { 
        this.rendered = true;
    }
}
</script>

<style>

</style>