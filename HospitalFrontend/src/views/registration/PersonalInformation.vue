<template>
    <fieldset v-if="countriesFetched">
        <legend></legend>
        <h2 class="fs-title">Personal Information</h2>
        <h3 class="fs-subtitle">We will never sell it</h3>
        <div class="row">
            <div class="col">
                <input type="text" name="fname" placeholder="First Name" v-model="personalInformation.Name"/>
                <input type="text" name="midname" placeholder="Middle Name" v-model="personalInformation.MiddleName" />
                <input type="text" name="lname" placeholder="Last Name" v-model="personalInformation.Surname" />
                <input type="text" name="pin" placeholder="Personal identity number" v-model="personalInformation.Jmbg" />
                <input type="text" name="inumber" placeholder="Insurance number" v-model="personalInformation.InsuranceNumber" />
                <input type="date" name="dateofbirth" placeholder="Date of birth" v-model="personalInformation.DateOfBirth" />
                <input type="text" name="phone" placeholder="Phone" v-model="personalInformation.TelephoneNumber" />
            </div>
            <div class="col">
                <select name="maritalstatus" required v-model="personalInformation.MaritalStatus">
                    <option value="" disabled selected>Marital status</option>
                    <option value="Married">Married</option>
                    <option value="Single">Single</option>
                    <option value="Widowed">Widowed</option>
                    <option value="Divorced">Divorced</option>
                </select>
                <select name="gender" required v-model="personalInformation.Gender">
                    <option value="" disabled selected>Gender</option>
                    <option value="Male">Male</option>
                    <option value="Female">Female</option>
                    <option value="Other">Other</option>
                </select>
                 <select name="rescountry" required v-on:change="fetchCityOfResidence">
                    <option value="" disabled selected>Country of residence</option>
                    <option v-bind:value="country.id" v-for="(country,index) in countries" v-bind:key="index">{{country.name}}</option>
                </select>
                <select name="rescity" required v-if="!cityOfResidenceDisabled" v-on:change="bindCityOfResidence">
                    <option value="" disabled selected>City of residence</option>
                    <option v-bind:value="city.id" v-for="(city,index) in citiesOfResidence" v-bind:key="index">{{city.name}}</option>
                </select>
                <input type="text" name="address" placeholder="Address" v-model="personalInformation.Address" />
                <select name="birthcountry" v-on:change="fetchCityOfBirth" required>
                    <option value="" disabled selected>Country of birth</option>
                    <option v-bind:value="country.id" v-for="(country,index) in countries" v-bind:key="index">{{country.name}}</option>
                </select>
                <select name="birthcity" required v-if="!cityOfBirthDisabled" v-on:change="bindCityOfBirth">
                    <option value="" disabled selected>City of birth</option>
                    <option v-bind:value="city.id" v-for="(city,index) in citiesOfBirth" v-bind:key="index">{{city.name}}</option>
                </select>

            </div>
        </div>

            <input type="button" name="next" class="next action-button" value="Next" @click="goToNextStep()"/>
    </fieldset>
</template>

<script>
import api from '../../constant/api.js'
import axios from 'axios'
import Toastify from 'toastify-js'

export default {
    name:"PersonalInformation",
    data:function(){
       return{
           personalInformation:{
            Name:'',
            MiddleName:'',
            Surname:'',
            Jmbg:'',
            InsuranceNumber:'',
            DateOfBirth:'',
            TelephoneNumber:'',
            Address:'',
            MaritalStatus:'',
            Gender:'',
            CountryOfResidenceId:null,
            CityOfResidenceId:null,
            CountryOfBirthId:null,
            CityOfBirthId:null,
           },
           countries:null,
           countriesFetched:false,
           cityOfResidenceDisabled:true,
           cityOfBirthDisabled:true,
           citiesOfResidence:null,
           citiesOfBirth:null
       } 
    },
    
    methods:{
        getAllCountries:function(){
            axios.get(api.countries).then(response=>{
                this.countries = response.data
                this.countriesFetched=true
            })
        },
        fetchCityOfResidence:function(e){
            this.personalInformation.CountryOfResidenceId = e.target.value
            axios.get(api.citiesByCountry + e.target.value).then(response => {
                this.citiesOfResidence=response.data
                this.cityOfResidenceDisabled=false
            })
        },
        fetchCityOfBirth:function(e){
            this.personalInformation.CountryOfBirthId = e.target.value
            axios.get(api.citiesByCountry + e.target.value).then(response => {
                this.citiesOfBirth=response.data
                this.cityOfBirthDisabled=false
            })
        },
        bindCityOfResidence:function(e){
            this.personalInformation.CityOfResidenceId = e.target.value
        },
        bindCityOfBirth:function(e){
            this.personalInformation.CityOfBirthId = e.target.value
        },
        areFieldsValid(){
            if(this.personalInformation.Name != '' && this.personalInformation.MiddleName!=''
             && this.personalInformation.Surname!='' && this.personalInformation.Jmbg!='' && this.personalInformation.InsuranceNumber!=''
             && this.personalInformation.DateOfBirth!='' && this.personalInformation.DateOfBirth!=''
             && this.personalInformation.TelephoneNumber!='' && this.personalInformation.Address!=''
             && this.personalInformation.MaritalStatus!='' && this.personalInformation.Gender!='' && this.personalInformation.CityOfResidenceId!=null
             && this.personalInformation.CountryOfResidenceId!=null && this.personalInformation.CityOfBirthId!=null && this.personalInformation.CountryOfBirthId!=null)
                return true
            return false
        },
        toastErrorMessage:function(message){
            Toastify({
                    text: message,
                    duration: '2000',
                    newWindow: true,
                    close: true,
                    gravity: 'top',
                    position: 'center',
                    backgroundColor: "linear-gradient(to right, #C37D92, #d89a9e)"
                }).showToast()
        }
        ,
        goToNextStep:function(){
            if(this.areFieldsValid()){
                this.$store.commit('setAccountDetails', this.personalInformation)
                this.$router.push('/register/account-details')
                
            }else{
                this.toastErrorMessage('You must fill all the fields before countinuing!')
            }
        }
    },
    mounted() {
        this.getAllCountries()
        this.personalInformation = this.$store.state.patientRegistrationDto.personalInformation
    },
}
</script>

<style>
     @import '../../styles/multipart-form-style.css';
</style>