<template>
    <fieldset>
        <h2 class="fs-title">Personal Details</h2>
        <h3 class="fs-subtitle">We will never sell it</h3>
        <div class="row">
            <div class="col">
                <input type="text" name="fname" placeholder="First Name" />
                <input type="text" name="midname" placeholder="Middle Name" />
                <input type="text" name="lname" placeholder="Last Name" />
                <input type="text" name="pin" placeholder="Personal identity number" />
                <input type="date" name="dateofbirth" placeholder="Date of birth" />
                <input type="text" name="phone" placeholder="Phone" />
                <textarea name="address" placeholder="Address"></textarea>
            </div>
            <div class="col">
                <select name="drinks" required>
                    <option value="" disabled selected>Marital status</option>
                    <option value="married">Married</option>
                    <option value="single">Single</option>
                    <option value="widowed">Widowed</option>
                    <option value="divorced">Divorced</option>
                </select>
                <select name="gender" required>
                    <option value="" disabled selected>Gender</option>
                    <option value="male">Male</option>
                    <option value="female">Female</option>
                    <option value="other">Other</option>
                </select>
                <select name="rescity" required>
                    <option value="" disabled selected>City of residence</option>
                    <option value="male">Novi Sad</option>
                    <option value="female">Beograd</option>
                    <option value="other">Sremska Mitrovica</option>
                </select>
                <select name="birthcity" required>
                    <option value="" disabled selected>City of birth</option>
                    <option value="male">Novi Sad</option>
                    <option value="female">Beograd</option>
                    <option value="other">Sremska Mitrovica</option>
                </select>
            </div>
                
        </div>
        <router-link to="/register/health-status">
            <input type="button" name="previous" class="previous action-button" value="Previous" />
        </router-link>
        <router-link to="/register/profile-picture">
            <input type="button" name="next" class="next action-button" value="Next" />
        </router-link>
    </fieldset>
</template>

<script>
export default {
    name:"AccountDetails"
}
</script>

<style>
    @import '../../styles/registration-style.css';
</style>