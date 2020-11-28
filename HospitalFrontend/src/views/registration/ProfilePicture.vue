<template>
    <link rel="stylesheet" type="text/css" href="https://cdn.jsdelivr.net/npm/toastify-js/src/toastify.min.css">
    <fieldset>
        <h2 class="fs-title">Profile Picture</h2>
        <div class="container">
            <div v-if="imageUrl">
                <img class="image-preview rounded-circle m-2" v-bind:src="imageUrl"/>
            </div>
            <!--UPLOAD-->
            <form enctype="multipart/form-data">
                <div class="custom-file">
                    <input type="file" single name="profile-picture" id="profile-picture" class="custom-file-input" :disabled="isSaving" @change="fileChange($event.target.files)" accept="image/*">
                    <label class="custom-file-label" for="profile-picture">
                    <p v-if="!isSaving">
                        Drag your image here.
                    </p>
                    <p v-else>
                        Uploading image...
                    </p></label>
                </div>
            </form>
        </div>
        <input type="button" name="previous" class="previous action-button" value="Previous" @click="goToPreviousPage()"/>
        <input type="button" name="submit" class="submit action-button" value="Submit" @click="registerPatient()" />
    </fieldset>
</template>

<script>
import axios from 'axios'
import Toastify from 'toastify-js'
import api from '../../constant/api.js'
import moment from 'moment'

export default {
    data: function () {
        return {
            name: "ProfilePicture",
            isSaving: false,
            uploadedImage: null,
            imageUrl: ''
        }
    },
    methods: {
        fileChange: function (image) {
            if (!image.length) return
            let formData = new FormData()
            console.log(image);
            formData.append("image", image[0])

            this.save(formData)
        },
        save: function (formData) {
            this.isSaving = true;
            axios.post(api.imageUpload, formData, {
                headers: {
                    Authorization: api.authorization
                },
            })
                .then(response => {
                    this.isSaving = false
                    this.imageUrl = response.data.data.link
                    this.toastSuccess("Image successfully uploaded!")
                })
                .catch(err => {
                    this.isSaving = false
                    this.toastError("An error ocurred while uploading an image!")
                })
        },
        toastSuccess:function(succesMsg){
        Toastify({
              text: succesMsg,
              duration: '2000',
              newWindow: true,
              close: true,
              gravity: 'top',
              position: 'center',
              backgroundColor: "linear-gradient(to right, #00b09b, #7ecc92)"
            }).showToast()
      },
      toastError:function(errorMsg){
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
      ,
      goToPreviousPage:function(){
          this.$router.push('/register/account-details')
      }
      ,
      isImageUrlPresent(){
        if(this.imageUrl=="")
            return false
        return true
      }
      ,
      registerPatient:function(){
        if(!this.isImageUrlPresent()){
            this.toastError('Upload your profile picture!')
            return
        }
        this.postPatientAccount()
        
      }
      ,
      postPatientAccount:function(){
        let patientAccountDto={...this.$store.state.patientRegistrationDto.PersonalInformation, ...this.$store.state.patientRegistrationDto.accountDetails}
        var today = moment()
        var dateOfBirth = moment(patientAccountDto["DateOfBirth"],'YYYY-MM-DD')
        patientAccountDto["Age"] = today.diff(dateOfBirth,'years')
        patientAccountDto["Citizenships"] = []
        patientAccountDto["Allergies"] = []
        patientAccountDto["AvatarUrl"] = this.imageUrl
        
         axios.post(api.patientRegistration,patientAccountDto).then(response=>{
            this.toastSuccess('Account successfully created!')
            this.toastSuccess('Please check your email so you can finish your registration')
            this.$router.push('/register')
         })

      }
      
    }
    
}
</script>

<style>

</style>