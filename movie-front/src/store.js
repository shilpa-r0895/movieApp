import Vue from "vue";
import Vuex from "vuex";

Vue.use(Vuex);


 const state = {

  category : "",
  movieDetails : {
    name : "",
    plot : "",
    url : "",
    year : "",
    actor : [],
    producer : "",
    
  },
  actorDetails : {
    name : "",
    bio : "",
    date : "",
    sex : ""
  },
  producerDetails : {
    name : "",
    bio : "",
    date : "",
    sex : ""
  },
  actors : [],
  producers  : [],
  movies :[],
  showModal : false,
  activeView : "movie",
  showModalContainer : false,
  showActorModal : false,
  showProducerModal : false,
  showMore : false,
  index : ""

 }

 const mutations = {
    changeView(state, activeView){
      state.activeView = activeView;
      state.category = activeView;
    },
    showSecondModal(state, category){
      state.showModalContainer = true;
      state.category = category;
    },
    hideModal(state){
      state.showModalContainer = false;
      state.showProducerModal = false;
      state.showActorModal = false;
    },
    showModal(state, category){
      if(category == "actors"){
        state.showActorModal = true;
        state.showProducerModal = false;
      }else{
        state.showProducerModal = true;
        state.showActorModal = false;
      }
    }, 
    addActor(state, data){
      state.actors = state.actors.concat(data);
    },
    addProducer(state, data){
      state.producers = state.producers.concat(data);
    },
    addMovie(state, data){
      state.movies = state.movies.concat(data);
    },

    showDetails(state, data){
      state.index = data;
      state.showMore = true;
    },
    hideShowDetails(state){
      state.showMore = false;
    }
 }

 const actions = {
      changeView(context, payload){
         
        context.commit('changeView', payload)
      },

      showSecondModal(context, payload){
        context.commit('showSecondModal', payload)
      },

      hideModal(context){
        context.commit('hideModal');
      },

      showModal(context, payload){
        context.commit('showModal', payload);
      },

      addActor(context, payload){
        context.commit('addActor', payload);

      },

      addProducer(context, payload){
        context.commit('addProducer', payload);
      },

      addMovie(context, payload){
        context.commit('addMovie', payload);
      },

      showDetails(context, payload){
        context.commit('showDetails', payload);
      },

      hideShowDetails(context){
        context.commit('hideShowDetails');
      }
 }

 const getters = {
    movies: state => {
        return state.movies;
    },
    actors: state => {
      return state.actors;
    },
    producers: state => {
      return state.producers;
    },
    category : state => {
      return state.category;
    },
    movieDetails : state => {
      return state.movieDetails;
    },
    actorDetails : state => {
      return state.actorDetails;
    },
    producerDetails : state => {
      return state.producerDetails;
    },
    showModal : state => {
      return state.showModal;
    },
    activeView : state => {
      return state.activeView;
    },
    showModalContainer : state => {
      return state.showModalContainer;
    },
    showActorModal : state => {
      return state.showActorModal;
    },
    showProducerModal : state => {
      return state.showProducerModal;
    },
    index : state => {
      return state.index;
    },
    showMore : state => {
      return state.showMore;
    }
    
 } 


export default new Vuex.Store({
  state,
  mutations,
  actions,
  getters
});
