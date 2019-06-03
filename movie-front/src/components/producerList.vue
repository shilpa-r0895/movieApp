<template>
    <div>
        <div v-if="producers.length">
            <b-list-group >
                <b-list-group-item v-for="(producer, index) in producers" :key="index" class="flex-column align-items-start">
                    <div class="d-flex w-100 justify-content-between">
                        <h5 class="mb-1">{{producer.name}}</h5>
                    </div>

                    <p class="mb-1">
                    {{producer.bio}}
                    </p>

                    <div>
                        <span>{{getDate(producer.date)}}</span>
                    </div>
                    <div>
                        <span>{{producer.sex}}</span>
                    </div>
                    <div class="icons">
                        <b-button variant="primary" class="icon-button" pills @click="editProducerDetails(index)"><v-icon name="edit"></v-icon></b-button>
                        <b-button variant="primary" class="icon-button" pills @click="deleteProducers(index)"><v-icon name="trash"></v-icon></b-button>
                    </div>

                </b-list-group-item>
            </b-list-group>
        </div>
         <div v-else>
            No Producers Added
        </div>
    </div>
</template>

<script>
import {mapGetters, mapActions} from 'vuex';

export default {

    computed:{
        ...mapGetters(["producers"])
    },
    methods : {
        ...mapActions(["editProducerModal", "deleteProducer"]),
         getDate(dt){
            var dob = new Date(dt);
            var d = dob.getDate() < 10 ? "0" + dob.getDate() : dob.getDate();
            var m = dob.getMonth() < 10 ? '0' + dob.getMonth() : dob.getMonth();
            var finalDOB = d + '/' + m + '/' + dob.getFullYear();
            return finalDOB;
        },
        editProducerDetails(index){
            this.editProducerModal({
                isEdit : true,
                producerDetails : this.producers[index],
                index : index
            });
        },
        deleteProducers(index){
            this.deleteProducer(index);
        }
    }
    
}
</script>
