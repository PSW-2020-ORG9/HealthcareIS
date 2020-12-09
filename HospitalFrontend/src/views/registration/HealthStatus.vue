<template>
    <fieldset>
        <h2 class="fs-title">Social Profiles</h2>
        <h3 class="fs-subtitle">Your presence on the social network</h3>
        <input type="text" name="twitter" placeholder="Twitter" />
        <input type="text" name="facebook" placeholder="Facebook" />
        <input type="text" name="gplus" placeholder="Google Plus" />
        <router-link to="/register/">
            <input type="button" name="previous" class="previous action-button" value="Previous" />
        </router-link>
        <router-link to="/register/account-details">
            <input type="button" name="next" class="next action-button" value="Next" />
        </router-link>
    </fieldset>
</template>

<script>
export default {
    name:"HealthStatus"
}
</script>

<style>
    @import '../../styles/multipart-form-style.css';
</style>