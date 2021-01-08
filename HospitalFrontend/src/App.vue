<template>
<body>
  <nav class="navbar navbar-expand-lg navbar-light" style="background-color:#d1ffbd">
    <a class="navbar-brand"><router-link to="/">HealthcareIS</router-link></a>
    <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbar-content" aria-controls="navbar-content" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse" id="navbar-content">
      <ul class="navbar-nav mr-auto">
        <li class="nav-item active">
          <router-link class="nav-link" to="/scheduling-type">Schedule Appointment</router-link>
        </li>
        <li class="nav-item active">
          <router-link class="nav-link" to="/feedbacks">Feedbacks</router-link>
        </li>
        <li class="nav-item active">
          <router-link class="nav-link" to="/survey-preview">Surveys</router-link>
        </li>
      </ul>
      <li class="navbar-nav nav-item dropdown">
        <a v-if="user" class="nav-link dropdown-toggle" href="#" id="navbarDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
          {{user.name + " " + user.surname}}
        </a>
        <router-link v-else to="/register" class="nav-link">Register</router-link>
        <div class="dropdown-menu dropdown-menu-right" aria-labelledby="navbarDropdown">
          <router-link to="/medical-record" class="dropdown-item">Profile</router-link>
          <router-link to="/examinations" class="dropdown-item">Examinations</router-link>
          <router-link to="/doc-search" class="dropdown-item">Documentation </router-link>
          <div class="dropdown-divider"></div>
          <a class="dropdown-item" href="#">Log out</a>
        </div>
      </li>
    </div>
  </nav>
  <router-view/>
</body>
</template>

<script>
export default {
  data: function () {
    return {
      user: null
    }
  },
  mounted: function () {
    //temporary
    this.$store.commit("setUser", {
      name: "Nikola",
      surname: "Pantelic"
    })
    this.user = this.$store.state.user
  }
}
</script>

<style lang="scss">
body {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  min-height: 100vh;
  text-align: center;
  color: #2c3e50;

}
#nav {
  padding: 30px;

  a {
    font-weight: bold;
    color: #285228;

    &.router-link-exact-active {
      color: #273f27;
      font-size: 18px;
      font-weight: bold;
    }
  }
}

</style>
