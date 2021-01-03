<template>
    <h1>Patient Medical Record</h1>
    <div v-if="patient" class="d-flex justify-content-center">
        <div>
            <div class="row">
                <div style="width: 15vw">
                    <img class="w-100 rounded p-1 border border-light" v-bind:src="profilePicture" alt=""/>
                </div>
                <table class="table w-auto text-left bg-light m-1">
                    <caption></caption>
                    <thead>
                        <tr>
                            <th colspan="2" id="personal_data_th">Personal data</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <th class="text-info" id="name_th1">Name:</th>
                            <td>{{patient.person.name}}</td>
                        </tr>
                        <tr>
                            <th class="text-info" id="last_name_th">Last name:</th>
                            <td>{{patient.person.surname}}</td>
                        </tr>
                        <tr>
                            <th class="text-info" id="middle_name_th">Middle name:</th>
                            <td>{{patient.person.middleName}}</td>
                        </tr>
                        <tr>
                            <th class="text-info" id="jmgb_th">JMBG:</th>
                            <td>{{patient.person.id}}</td>
                        </tr>
                        <tr>
                            <th class="text-info" id="insurance_number_th">Insurance number:</th>
                            <td>{{patient.insuranceNumber}}</td>
                        </tr>
                        <tr>
                            <th class="text-info" id="country_th">Country:</th>
                            <td>{{patient.person.cityOfResidence.country.name}}</td>
                        </tr>
                        <tr>
                            <th class="text-info" id="address_th">Address:</th>
                            <td>{{patient.person.cityOfResidence.name + ', ' + patient.person.address}}</td>
                        </tr>
                        <tr>
                            <th class="text-info" id="date_of_birth_th">Date of birth:</th>
                            <td>{{parseDate(patient.person.dateOfBirth)}}</td>
                        </tr>
                        <tr>
                            <th class="text-info" id="city_of_birth_th">City of birth:</th>
                            <td>{{patient.person.cityOfBirth.name}}</td>
                        </tr>
                        <tr>
                            <th class="text-info" id="phone_number_th">Phone number:</th>
                            <td>{{patient.person.telephoneNumber}}</td>
                        </tr>
                    </tbody>
                </table>
                <table class="table text-left w-auto bg-light m-1">
                    <caption></caption>
                    <thead>
                        <tr>
                            <th colspan="2" id="allergies_th">Allergies</th>
                        </tr>
                    </thead>
                    <tbody v-if="patient.allergies.length == 0">
                        <th id="no_recorded_allergies_th">No recorded patient allergies.</th>
                    </tbody>
                    <tbody v-else>
                        <tr>
                            <th class="text-info" id="allergen_th">Allergen</th>
                            <th class="text-info" id="intensity_th">Intensity</th>
                        </tr>
                        <tr v-for="allergyManifestation in patient.allergies" v-bind:key="allergyManifestation.id">
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
                    <caption></caption>
                    <thead>
                        <th colspan="2" id="diagnosis_history_th">Diagnosis history</th>
                    </thead>
                    <tbody v-if="examinations.length == 0">
                        <th id="no_recorded_history_th">No recorded patient history.</th>
                    </tbody>
                    <tbody v-else>
                        <tr>
                            <th class="text-info" id="name_th2">Name</th>
                            <th class="text-info" id="description_th">Description</th>
                        </tr>
                        <tr v-for="diagnosis in getDiagnoses()" v-bind:key="diagnosis.id">
                            <td>{{diagnosis.name}}</td>
                            <td>{{diagnosis.description}}</td>
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
            profilePicture: '',
            intensities: ["Mild", "Medium", "Strong", "Severe"],
            examinations: []
        }
    },
    emits: ['updateExaminations'],
    methods: {
        getPatient: function (id) {
            let url = api.patient + '/' + id
            axios.get(url).then(response => {
                this.patient = response.data
                this.fetchProfilePicture();
            })

            this.getExaminations(id)
        },
        getExaminations:function(id){
           
            axios.get(api.examination+"/patient/"+id).then(response=>{
                this.examinations=response.data
            })
        }
        ,
        parseDate: function (date) {
            let dateObj = new Date(date)
            let parsedDate = "" + dateObj.getDate() + "." + (dateObj.getMonth() + 1) + "." + dateObj.getFullYear() + "."
            return parsedDate
        },
        getDiagnoses: function () {
            let diagnoses = []
            this.patient.examinations.forEach(examination => {
                if (examination.examinationReport) {
                    examination.examinationReport.diagnoses.forEach(diagnosis =>
                        diagnoses.push(diagnosis)
                    )
                }
            })
            return diagnoses
        },
        fetchProfilePicture: function () {
            let url = api.patientAccount + '/' + this.patient.id
            
            axios.get(url).then(response => {
                this.profilePicture = response.data.avatarUrl
            })

        }
    },
    mounted: function () {
        this.getPatient(4);
    }
}
</script>