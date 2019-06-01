<template>
<div>
    <div v-if="movies.length" >
        <b-card v-for="(movie, index) in movies" :key="index"
            :title="movie.name"
            :img-src="movie.url"
            img-alt="Image"
            img-top
            tag="article"
            style="max-width: 20rem;"
            class="mb-2"
        >
            <b-card-text>
            {{movie.plot}}
            </b-card-text>

            <b-button variant="primary" @click="showMovieDialog(index)">Show Details</b-button>
        </b-card>
    </div>
    <div v-else>
        No movies to show
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
        ...mapActions(["showDetails"]),
        showMovieDialog(index){
            this.showDetails(index);
        }
    }
}
</script>
