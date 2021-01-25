<template>
	<link rel="stylesheet" type="text/css" href="https://cdn.jsdelivr.net/npm/toastify-js/src/toastify.min.css">
	<form id="msform">
    <ul id="progressbar">
        <li v-bind:class="{active : isDateSelect()}">Date Selection</li>
        <li v-bind:class="{active : isDoctorSelect()}">Doctor Selection</li>
		<li v-bind:class="{active : isAppointmentSelect()}">Appointment Selection</li>
    </ul>
	<router-view v-slot="slotProps">
		<transition name="router-anim" enter-active-class="animate__animated animate__fadeIn">
			<component :is="slotProps.Component"/>
		</transition>
	</router-view> 
	</form>
</template>

<script>
import { publishSchedulingEvent } from '../../../services/eventPublisher.js'
export default {
	name:"ScheduleAppointment",
	methods:{
		isDateSelect:function(){
			return this.$route.name === 'chooseDate'
		},
		isDoctorSelect:function(){
			return this.$route.name === 'chooseDoctor'
		},
		isAppointmentSelect:function(){
			return this.$route.name === 'chooseAppointment'
		}
	}
	
}

</script>

<style>
   @import '../../../styles/multipart-form-style.css';
   @import 'https://cdnjs.cloudflare.com/ajax/libs/animate.css/4.1.1/animate.min.css';
</style>