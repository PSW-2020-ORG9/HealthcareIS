<template>
  <fieldset>
        <h2 class="fs-title">Appointment selection</h2>
        <select name="appointments">
            <option value="">Select an appointment...</option>
            <!--option v-bind:value="index" v-for="(appointment,index) in availableAppointments"
            @click="selectAppointment(appointment)" v-bind:key="index">{{this.formAppointmentString(appointment)}}</option-->
        </select>
        <input type="button" name="previous" class="previous action-button" value="Previous" @click="goToPrevPage()" />
        <input type="button" name="next" class="next action-button" value="Schedule" />
    </fieldset>
</template>

<script>
export default {
    name:'ChooseRecommended',
    data:function(){
        availableAppointments:[]
    },
    methods: {
        goToPrevPage:function(){
            this.$router.push('/recommend-appointment/')
        }
    },
}
</script>

<style>

</style>