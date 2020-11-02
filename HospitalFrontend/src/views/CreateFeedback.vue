<template>

  <div class="container">
    <div class="col-sm-3 mx-auto shadow-lg p-3 mb-5 bg-white rounded feedback">
      <div class="col-sm d-flex justify-content-start">
        <div class="form-group d-flex justify-content-start">
          <textarea class="form-control" rows=5 type="text" placeholder="Enter your feedback"></textarea>
        </div>
      </div>
      <div class="col-sm offset-md-1 d-flex justify-content-start">
        <div class="form-group">
          <input type="checkbox" class="form-check-input" id="public-feedback">
          <label class="form-check-label" for="public-feedback">Public</label>
        </div>
      </div>
      <div class="col-sm offset-md-1 d-flex justify-content-start">
        <div class="form-group">
          <input type="checkbox" class="form-check-input" id="anon-feedback">
          <label class="form-check-label" for="anon-feedback">Send anonimously</label>
        </div>
      </div>
      <button type="submit" class="btn submit-btn">Submit</button>
    </div>
  </div>

</template>

<script>
  export default {
    name: 'CreateFeedback',
    methods: {

    }
  }
</script>

<style>
.col-md-1-5 { width: 5,5%; }
.submit-btn {
  background-color: #58804f;
}

</style>