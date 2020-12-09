<template>
    <form id="msform">
        <h2 class="fs-title">Choose scheduling type</h2>
        <router-link to="/schedule-appointment">
            <input type="button" name="schedule" class="action-button-wide" value="Shedule an appointment"  />
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