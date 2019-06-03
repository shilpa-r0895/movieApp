<template>
<div>
    <div v-if="movies.length" class="flex" >
        <b-card v-for="(movie, index) in movies" :key="index"
            :title="movie.name"
            :img-src="movie.url"
            img-alt="Image"
            img-top
            tag="article"
            style="max-width: 20rem;"
            class="mb-2 card"
        >
            <b-card-text>
            {{movie.plot}}
            </b-card-text>
            <b-button variant="link" class="icon-button"  @click="editMovieDetails(index)"><v-icon name="edit"></v-icon></b-button>
            <b-button variant="link" class="icon-button"  @click="deleteMovies(index)"><v-icon name="trash"></v-icon></b-button>


            <b-button variant="primary" class="card-button" @click="showMovieDialog(index)">Show Details</b-button>
        </b-card>
    </div>
    <div v-else>
        No movies Added
    </div>
    <div v-if="showMore">
         <DetailModal/>
    </div>
</div>
</template>


<script>
import { mapActions, mapGetters } from "vuex";
import DetailModal from './detailModal.vue';

export default {
    
    computed : {
       ...mapGetters(['movies', 'showMore'])
    },
    components :{
        DetailModal
    },
    methods :{
        ...mapActions(["showDetails", "editMovie", "deleteMovie", 'showMovieModal']),
        showMovieDialog(index){
            this.showDetails(index);
        },
        editMovieDetails(index){
           this.showMovieModal({isMovieEdit : true, movieDetails : this.movies[index], index : index});

        },
        deleteMovies(index){
            this.deleteMovie(index)

        }
    }
}
</script>
