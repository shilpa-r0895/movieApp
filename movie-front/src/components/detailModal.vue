<template>
    <div>
         <b-modal  v-model="modalShow" scrollable  @hidden="resetModal" centered title="Movie Details" >
             <b-img v-if="this.getData.url" :src="this.getData.url" fluid alt="Movie Poster"></b-img>
                <div class="center">          
                    <div class="my-4 name">{{this.getData.name}}</div>
                    <div class="my-4">
                        <span>Release Year : </span>
                        {{this.getData.year}}
                    </div>
                    <div>
                        <span>Plot : </span>
                        <p>
                            {{this.getData.plot}}
                        </p>
                    </div>
                    <div class="flex">
                        <span>Actors : </span>
                    
                        <div v-for="(id, index) in this.getData.actor" :key="index" class="colored marLeft">
                            {{getActor(id)}} <span v-if="index < this.getActor.actor.length">,</span>
                        </div>
                     </div>

                    <div class="flex">
                         <span>Producer : </span>
                        <div class="colored marLeft">{{getProducer(this.getData.producer)}}</div>
                    </div>
                </div>
                 <template slot="modal-footer" >
                    <b-button size="sm" variant="primary" @click="resetModal()">
                        Back
                    </b-button>
                </template>

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
