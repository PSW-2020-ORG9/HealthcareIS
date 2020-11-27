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
        <router-link to="/register/account-details">
            <input type="button" name="previous" class="previous action-button" value="Previous" />
        </router-link>
        <input type="submit" name="submit" class="submit action-button" value="Submit" />
    </fieldset>
</template>

<script>
import axios from 'axios'
import Toastify from 'toastify-js'
import api from '../../constant/api.js'

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
                    this.toastSuccess()
                })
                .catch(err => {
                    this.isSaving = false
                    this.toastError()
                })
        },
        toastSuccess:function(){
        Toastify({
              text: "Image succesfully uploaded.",
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
              text: "Error while uploading image.",
              duration: '2000',
              newWindow: true,
              close: true,
              gravity: 'top',
              position: 'center',
              backgroundColor: "linear-gradient(to right, #C37D92, #d89a9e)"
            }).showToast()
      }
    }
}
</script>

<style>

</style>