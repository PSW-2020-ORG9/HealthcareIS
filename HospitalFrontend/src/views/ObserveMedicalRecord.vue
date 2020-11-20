<template>
    <h1>Patient Medical Record</h1>
    <div class="container">
        <div class="row">Ime: {{patient.name}}</div>
        <div class="row">Prezime: {{patient.surname}}</div>
        <div class="row">Ime roditelja: {{patient.middlename}}</div>
    </div>
</template>

<script>
import api from '../constant/api.js'
import axios from 'axios'

export default {
    data: function () {
        return {
            patient: null
        }
    },
    methods: {
        getPatient: function (id) {
            let url = api.patient + '/find/' + id
            axios.get(url).then(response => {
                this.patient = response.data
            })
        }
    },
    mounted: function () {
        this.getPatient(2);
    }
}
</script>

<style>

</style>