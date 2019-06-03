<template>
    <div>
         <b-modal  v-model="modalShow"  @hidden="resetModal" centered >
             <b-img v-if="this.getData.url" :src="this.getData.url" fluid alt="Movie Poster"></b-img>
                            
                    <div class="my-4">{{this.getData.name}}</div>
                    <div class="my-4">{{this.getData.year}}</div>
                    <p>
                        {{this.getData.plot}}
                    </p>
                    <div v-for="(id, index) in this.getData.actor" :key="index">
                        {{getActor(id)}}
                    </div>
                    <div>
                        {{getProducer(this.getData.producer)}}
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
             return this.movies[this.index];                   
        }
    },
    methods : {
        ...mapActions(['hideShowDetails']),
        resetModal(){
            this.hideShowDetails();
        },
        getActor(id){
           var actor = this.actors.find((a) => a.id == id);
           return actor.name;
        },
        getProducer(id){
            var pro = this.producers.find((p) => p.id == id);
            return pro.name
        }
    }
    
    
}
</script>
