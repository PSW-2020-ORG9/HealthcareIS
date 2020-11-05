<template>


    <div class="card">
        <h5 class="card-header">{{feedback.UserAccount.Username}}}</h5>
        <div class="card-body">           
            <p class="card-text">{{feedback.UserAccount}}</p>
            <a href="#" class="btn btn-primary">Go somewhere</a>
        </div>
    </div>

</template>

<script>


export default {
  name:'ObserveFeedback',
  props:{
      feedback: Object
  },

  
  methods: {},
  mounted: function(){
    
      
    
  }
}
</script>

<style>

</style>