<template>
<link rel="stylesheet" type="text/css" href="https://cdn.jsdelivr.net/npm/toastify-js/src/toastify.min.css">
<form id="msform">
    <transition name="router-anim" enter-active-class="animate__animated animate__fadeIn">
    <fieldset>
        <legend></legend>
        <h2 class="fs-subtitle">Please enter your login information bellow</h2>
        <div class="row">
            <div class="col">
                <input type="text" name="email" v-model="email" placeholder="E-Mail"/>
                <input type="password" name="password" v-model="password" placeholder="Password"/>
            </div>
        </div>
        <input type="button" class="action-button" @click="login" value="Login"/><br/>
        <router-link to="/register">Register here.</router-link>
    </fieldset>
    </transition>
</form>
</template>

<script>
import axios from 'axios'
import api from '../constant/api'
import Toastify from 'toastify-js'
import { parseJwt, setUser } from '../jwt.js'

export default {
    data: function () {
        return {
            email: '',
            password: ''
        }
    },
    methods : {
        login: function () {
            axios.post(api.loginUrl, {
                email : this.email,
                password : this.password
            })
            .then(response => {
                setUser(this)
                if (this.$store.state.user.role == 'Admin')
                    this.$router.push('/feedbacks')
                else this.$router.push('/')
            })
            .catch(error => this.toastError(error))
        },
        toastError: function (errorMsg) {
            Toastify({
              text: errorMsg,
              duration: '2000',
              newWindow: true,
              close: true,
              gravity: 'top',
              position: 'center',
              backgroundColor: "linear-gradient(to right, #C37D92, #d89a9e)"
            }).showToast()
        }
    },
}
</script>

<style>
     @import '../styles/multipart-form-style.css';
</style>