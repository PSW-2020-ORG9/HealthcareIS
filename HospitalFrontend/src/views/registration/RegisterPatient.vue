<template>
	<form id="msform">
    <ul id="progressbar">
        <li v-bind:class="{active : isPersonalInfo()}">Personal Information</li>
        <li v-bind:class="{active : isHealthStatus()}">Health Status</li>
        <li v-bind:class="{active : isAccDetails()}">Account Details</li>
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
	name:"RegisterPatient",
	methods:{
		isPersonalInfo:function(){
			return this.$route.name === 'personalInformation'
		},
		isHealthStatus:function(){
			return this.$route.name === 'healthStatus'
		},
		isAccDetails:function(){
			return this.$route.name === 'accountDetails'
		}

	}
}

</script>

<style>
   @import '../../styles/registration-style.css';
   @import 'https://cdnjs.cloudflare.com/ajax/libs/animate.css/4.1.1/animate.min.css';
</style>