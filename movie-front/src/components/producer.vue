<template>
     <div>
         <b-button variant="primary" class="addButton" @click="this.showForm">Add Producer</b-button>
        <div class="actorContainer">
            <div  class="list">
                <ProducerList/>
            </div>
         </div>
        <div v-if="this.showProducerModal">
            <ModalContainer/>
        </div>
    </div>
</template>

<script>

import { mapActions, mapGetters } from "vuex";
import ModalContainer from "./modalContainer.vue";
import ProducerList from './producerList.vue';

export default {
    computed:{
        ...mapGetters(['showProducerModal'])
    },
    components :{
        ProducerList,
        ModalContainer
    },
    methods : {
         ...mapActions(["showModal"]),
         showForm(){
            this.showModal('producers')
        }
    }
}
</script>

