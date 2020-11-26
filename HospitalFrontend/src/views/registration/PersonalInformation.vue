<template>
    <fieldset>
        <h2 class="fs-title">Social Profiles</h2>
        <h3 class="fs-subtitle">Your presence on the social network</h3>
        <input type="text" name="twitter" placeholder="Twitter" />
        <input type="text" name="facebook" placeholder="Facebook" />
        <input type="text" name="gplus" placeholder="Google Plus" />
        <router-link to="/register/health-status">
            <input type="button" name="next" class="next action-button" value="Next" />
        </router-link>
    </fieldset>
</template>

<script>
export default {
    name:"PersonalInformation"
}
</script>

<style>
    @import '../../styles/registration-style.css';
</style>