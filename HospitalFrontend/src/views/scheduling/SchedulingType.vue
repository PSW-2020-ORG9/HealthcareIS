<template>
<link rel="stylesheet" type="text/css" href="https://cdn.jsdelivr.net/npm/toastify-js/src/toastify.min.css">
    <form id="msform">
        <h2 class="fs-title">Choose scheduling type</h2>
        <router-link to="/schedule-appointment">
            <input type="button" name="schedule" class="action-button-wide" value="Schedule an appointment"  />
        </router-link>
        <router-link to="/recommend-appointment">
            <input type="button" name="recommend" class="action-button-wide" value="Recommend me an appointment"  />
        </router-link>
    </form>
</template>

<script>
export default {
    name:'SchedulingType'
}
</script>

<style>
    @import '../../styles/multipart-form-style.css';
</style>