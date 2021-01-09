<template>
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
export default {
    data: function () {
        return {
            email: '',
            password: ''
        }
    },
    login: function () {
        // users
    }
}
</script>

<style>
     @import '../styles/multipart-form-style.css';
</style>