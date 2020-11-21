<template>
    <h1>Patient Medical Record</h1>
    <div v-if="patient" class="d-flex justify-content-center">
        <div>
            <div class="row">
                <table class="table w-auto text-left bg-light m-1">
                    <thead>
                        <tr>
                            <th colspan="2">Personal data</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <th class="text-info">Name:</th>
                            <td>{{patient.person.name}}</td>
                        </tr>
                        <tr>
                            <th class="text-info">Last name:</th>
                            <td>{{patient.person.surname}}</td>
                        </tr>
                        <tr>
                            <th class="text-info">Middle name:</th>
                            <td>{{patient.person.middleName}}</td>
                        </tr>
                        <tr>
                            <th class="text-info">JMBG:</th>
                            <td>{{patient.person.jmbg}}</td>
                        </tr>
                        <tr>
                            <th class="text-info">Insurance number:</th>
                            <td>{{patient.insuranceNumber}}</td>
                        </tr>
                        <tr>
                            <th class="text-info">Country:</th>
                            <td>{{patient.person.cityOfResidence.country.name}}</td>
                        </tr>
                        <tr>
                            <th class="text-info">Address:</th>
                            <td>{{patient.person.cityOfResidence.name + ' ' + patient.person.address}}</td>
                        </tr>
                        <tr>
                            <th class="text-info">Date of birth:</th>
                            <td>{{parseDate(patient.person.dateOfBirth)}}</td>
                        </tr>
                        <tr>
                            <th class="text-info">Phone number:</th>
                            <td>{{patient.person.telephoneNumber}}</td>
                        </tr>
                    </tbody>
                </table>
                <table class="table text-left w-auto bg-light m-1">
                    <thead>
                        <tr>
                            <th colspan="2">Allergies</th>
                        </tr>
                    </thead>
                    <tbody v-if="patient.medicalHistory.allergies.length == 0">
                        <th>No recorded patient allergies.</th>
                    </tbody>
                    <tbody v-else>
                        <tr>
                            <th class="text-info">Allergen</th>
                            <th class="text-info">Intensity</th>
                        </tr>
                        <tr v-for="allergyManifestation in patient.medicalHistory.allergies" v-bind:key="allergyManifestation.id">
                            <td>
                                {{allergyManifestation.allergy.allergen}}
                            </td>
                            <td>
                                {{intensities[allergyManifestation.intensity]}}
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="row">
                <table class="table text-left bg-light m-1">
                    <thead>
                        <th colspan="3">Diagnosis history</th>
                    </thead>
                    <tbody v-if="patient.medicalHistory.personalHistory.diagnoses.length == 0">
                        <th>No recorded patient history.</th>
                    </tbody>
                    <tbody v-else>
                        <tr>
                            <th class="text-info">Name</th>
                            <th class="text-info">Type</th>
                            <th class="text-info">Description</th>
                        </tr>
                        <tr v-for="diagnosisDetails in patient.medicalHistory.personalHistory.diagnoses" v-bind:key="diagnosisDetails.id">
                            <td>{{diagnosisDetails.diagnosis.name}}</td>
                            <td>{{diagnosisTypes[diagnosisDetails.type]}}</td>
                            <td>{{diagnosisDetails.diagnosis.description}}</td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</template>

<script>
import api from '../constant/api.js'
import axios from 'axios'

export default {
    data: function () {
        return {
            patient: null,
            intensities: ["Mild", "Medium", "Strong", "Severe"],
            diagnosisTypes: ["Chronic", "Acute"]
        }
    },
    methods: {
        getPatient: function (id) {
            let url = api.patient + '/find/' + id
            axios.get(url).then(response => {
                this.patient = response.data
            })
        },
        parseDate: function (date) {
            let dateObj = new Date(date)
            let parsedDate = "" + dateObj.getDate() + "." + (dateObj.getMonth() + 1) + "." + dateObj.getFullYear() + "."
            return parsedDate
        }
    },
    mounted: function () {
        this.getPatient(1);
    }
}
</script>

<style>

</style>