<template>
<div class="container mt-5">
    <h2><img src="https://avatars.githubusercontent.com/u/73312486"><b> Healthcare Information System </b></h2>
    <h3 class="text-secondary"> Your health is our responsibility </h3>
    <div class="overflow-auto">
        <div id="feedbackList" class="list-group container w-50" >
            <FeedbackItem v-for="feedback in feedbacks" v-bind:key="feedback.Id" v-bind:feedback="feedback" v-on:update-feedbacks="getAllFeedbacks"></FeedbackItem>
        </div>
    </div>
</div>
</template>
<script>
import FeedbackItem from '../components/FeedbackItem.vue'
import axios from 'axios'
import api from '../constant/api.js'
export default {
    name: 'LandingPage',
    components: {
        FeedbackItem
    },
    data: function () {
        return {
            feedbacks: []
        }
    },
    methods: {
        getAllFeedbacks:function() {
            let url = api.feedback
            if (this.$store.state.user && this.$store.state.user.role !== "Admin")
                url +="/published"

            axios.get(url).then((response) => 
            {
                response.data.forEach(element => {
                element.date = this.formatDate(element.date)
                });
                this.feedbacks = response.data
            })      
        },
        formatDate : function(date) {
            let d = new Date(date)
            return d.getDate() + "/" + (d.getMonth()+1) + "/" + d.getFullYear()
        }
    },
    mounted: function() {
        this.getAllFeedbacks();
    }
}
</script>