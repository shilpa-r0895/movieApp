<template>
   <div class="App" id="app">
        <div>
          <b-navbar toggleable="lg" type="dark" variant="info">
            <b-navbar-brand href="#">Movie App</b-navbar-brand>
            <b-navbar-toggle target="nav-collapse"></b-navbar-toggle>
          </b-navbar>
        </div>
        <div class="main">
          <div class="navigation">
            <b-tabs pills tabs fill>
              <b-tab title="Movies" @click="changeViews('movie')" active></b-tab>
              <b-tab title="Actors" @click="changeViews('actors')"></b-tab>
              <b-tab title="Producers" @click="changeViews('producers')"></b-tab>
            </b-tabs>
          </div>
          <div class="middleContiner">
            <div v-if="this.activeView=='movie'">
              <Movie />
            </div>
            <div v-if="this.activeView=='actors'">
              <Actors />
            </div>
            <div v-if="this.activeView=='producers'">
              <Producers />
            </div>
          </div>
       </div>
        <b-alert show="5" dismissible fade v-if="this.showAlert">{{this.alertMsg}}</b-alert>
    </div>
</template>

<script>
import Movie from "./components/movie.vue";
import Actors from "./components/actors.vue";
import Producers from "./components/producer.vue";
import { mapActions, mapGetters } from "vuex";



export default {
  name: "app",
  components: {
    Movie,
    Actors,
    Producers
  },
  created() {
    this.getActors();
  },
  computed : {
      ...mapGetters(['activeView', 'showAlert', 'alertMsg'])
  },
   methods : {
      ...mapActions(["changeView", 'getActors']),
     changeViews(id){
        this.changeView(id);
     }
    
   }
};
</script>

<style lang="css" src="./css/app.css"></style>
<style lang="scss">
#app {
  font-family: "Avenir", Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}
</style>
