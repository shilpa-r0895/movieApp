<template>
    <div>
         <b-modal  v-model="modalShow"  @hidden="resetModal" centered >
             <b-img :src="this.getData.url" fluid alt="Movie Poster"></b-img>
                            
                    <div class="my-4">{{this.getData.name}}</div>
                    <div class="my-4">{{this.getData.year}}</div>
                    <p>
                        {{this.getData.plot}}
                    </p>
                    <div v-for="(actor, index) in this.getData.selectedActors" :key="index">
                        {{actor}}
                    </div>
                    <div>
                        {{this.getData.selectedProducer}}
                    </div>
            </b-modal>
    </div>
</template>

<script>
 import { mapActions, mapGetters } from "vuex";

export default {

    data(){
        return{
            modalShow : true
        }
    },
    computed: {
        ...mapGetters(['index', 'activeView', 'movies', 'actors', 'producers']),
        getData(){
            switch(this.activeView){
                case 'movie' :
                    return this.movies[this.index];
                    break;
                case 'actors' :
                    return this.actors[this.index];
                    break;
                case 'producers' : 
                    return this.producers[this.index];
            }
        }
    },
    methods : {
        ...mapActions(['hideShowDetails']),
        resetModal(){
            this.hideShowDetails();
        }
    }
    
    
}
</script>
