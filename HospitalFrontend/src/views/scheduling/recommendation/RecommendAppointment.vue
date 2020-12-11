<template>
<link rel="stylesheet" type="text/css" href="https://cdn.jsdelivr.net/npm/toastify-js/src/toastify.min.css">
    <form id="msform">
        <ul id="progressbar">
            <li v-bind:class="{active : isPrefActive()}" style="width:50%">Appointment Preferences</li>
            <li v-bind:class="{active : isSelectionActive()}" style="width:50%">Appointment Confirmation</li>
        </ul>
    
    <router-view v-slot="slotProps">
		<transition name="router-anim" enter-active-class="animate__animated animate__fadeIn">
			<component :is="slotProps.Component"/>
		</transition>
	</router-view>
    </form>
</template>

<script>
export default {
    name:'RecommendAppointment',
    methods: {
        isPrefActive:function(){
            return this.$route.name == "appointmentPreferences"
        },
        isSelectionActive:function(){
            return this.$route.name == "chooseRecommended"
        }
    },
}
</script>

<style>
    @import '../../../styles/multipart-form-style.css';
</style>