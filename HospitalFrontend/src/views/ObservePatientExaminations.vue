<template>
    <div></div>
</template>

<script>
import axios from 'axios'
import api from '../constant/api.js'

export default {
    data: function () {
        return {
            examinations: []
        }
    },
    mounted: function () {
        axios.get(api.patientExaminations + '/patient/1').then(response => {
            this.examinations = response.data
        })
    }
}
</script>

<style>

</style>