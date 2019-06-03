<template>
    <div>
         <b-button variant="primary" class="addButton" @click="this.showForm">Add Movie</b-button>
        <div class="movieContainer">
            <div  class="list">
                <MovieList/>
            </div>
            <div v-if="this.showFormDetails">
                <MovieDetails/>
            </div>
         </div>
        <div v-if="this.showModalContainer">
            <ModalContainer/>
        </div>
    </div>
</template>

<script>

import MovieList from "./movieList.vue";
import ModalContainer from "./modalContainer.vue";
import MovieDetails from './movieDetails.vue';
import { mapActions, mapGetters } from "vuex";

export default {
    name : "Movie",

    data(){
        return {
            movies : [],
            //showFormDetails : false
        }
    },
    components : {
        MovieList,
        ModalContainer,
        MovieDetails
    },
    computed:{
         ...mapGetters(['showModalContainer', 'showFormDetails'])
    },
    methods : {
        ...mapActions(['showMovieModal']),
        showForm(){
            this.showMovieModal();
        }
    }
    
}
</script>
