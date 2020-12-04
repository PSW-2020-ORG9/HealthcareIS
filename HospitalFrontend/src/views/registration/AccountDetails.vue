<template>
    <fieldset>
    <h2 class="fs-title">Account Details</h2>
    <h3 class="fs-subtitle">Enter your login credentials</h3>
    <input type="text" name="email" placeholder="Email" v-model="accountDetails.Email"/>
    <input type="text" name="username" placeholder="Username" v-model="accountDetails.Username" />
    <input type="password" name="pass" placeholder="Password" v-model="accountDetails.Password"/>
    <input type="password" name="cpass" placeholder="Confirm Password" v-model="PasswordConfirmation"/>
    <input type="button" name="previous" class="previous action-button" value="Previous" @click="goToPreviousPage()" />
    <input type="button" name="next" class="next action-button" value="Next" @click="goToNextPage()"/>
  </fieldset>
</template>

<script>
import Toastify from 'toastify-js'
export default {
    name:"AccountDetails",
    data:function(){
        return{
             accountDetails:{
                Username:'',
                Password:'',
                Email:''
            },
            PasswordConfirmation:''
        }
    }
    ,
    methods:{
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
        isPasswordValid:function(){
            return this.PasswordConfirmation==this.accountDetails.Password
        },
        isEmailValid:function(){
            if(/^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/.test(this.accountDetails.Email))
                return true
            return false
        }
        ,
        areAnyOfTheFieldsEmpty:function(){
            if(this.accountDetails.Email=="" || this.accountDetails.Username=="" || this.accountDetails.Password=="" || this.PasswordConfirmation=="")
                return true
            return false
        }
        ,
        goToNextPage:function(){
            if(this.areAnyOfTheFieldsEmpty()){
                this.toastErrorMessage('You must fill all the fields before continuing!')
                return
            }
            if(!this.isEmailValid()){
                this.toastErrorMessage('Email is not valid.')
                return
            }
            if(!this.isPasswordValid()){
                this.toastErrorMessage('Passwords must match!')
                return
            }
            

            this.$store.commit('setAccountDetails', this.accountDetails)
            this.$router.push('/register/profile-picture')
        }
        ,
        goToPreviousPage:function(){
            this.$store.commit('setAccountDetails', this.accountDetails)
            this.$router.push('/register/')
        }
    }
    ,
    mounted() {
        this.accountDetails = this.$store.state.patientRegistrationDto.accountDetails
    }

}
</script>

<style>
    @import '../../styles/multipart-form-style.css';
</style>