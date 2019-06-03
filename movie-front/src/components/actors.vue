<template>
     <div>
         <b-button variant="primary" class="addButton" @click="this.showForm">Add Actor</b-button>
        <div class="actorContainer">
            <div  class="list">
                <ActorList/>
            </div>
         </div>
        <div v-if="this.showActorModal">
            <ModalContainer/>
        </div>
    </div>
</template>

<script>

import { mapActions, mapGetters } from "vuex";
import ModalContainer from "./modalContainer.vue";
import ActorList from './actorList.vue';

export default {
    computed:{
        ...mapGetters(['showActorModal'])
    },
    components :{
        ActorList,
        ModalContainer
    },
    methods : {
         ...mapActions(["showModal"]),
         showForm(){
            this.showModal('actors')
        }
    }
}
</script>

