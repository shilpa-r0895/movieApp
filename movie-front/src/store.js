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
  showFormDetails : false,
  showModalContainer : false,
  showActorModal : false,
  showProducerModal : false,
  showMore : false,
  index : 0,
  isMovieEdit : false,
  isEdit : false

 }

 const mutations = {
    changeView(state, activeView){
      state.activeView = activeView;
      state.category = activeView;
    },
    showMovieModal(state, data){
      state.showFormDetails = true;
      if(data && data.isMovieEdit){
        state.isMovieEdit = data.isMovieEdit;
        state.movieDetails = data.movieDetails;
        state.index = datta.index;
      }
      
    },
    hideMovieModal(state){
      state.showFormDetails = false;
      if(state.isMovieEdit){
        state.isMovieEdit = false;
      }
    },
    showSecondModal(state, category){
      state.showModalContainer = true;
      state.category = category;
    },
    hideModal(state){
      state.showModalContainer = false;
      state.showProducerModal = false;
      state.showActorModal = false;
      if(state.isEdit){
        state.isEdit = false;
      }
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
      if(state.isEdit){
        state.actors[state.index] = data;
        state.isEdit = false;
      }else{
        state.actors = state.actors.concat(data);
      }
      
    },
    addProducer(state, data){
      if(state.isEdit){
        state.producers[state.index] = data;
        state.isEdit = false;
      }else{
        state.producers = state.producers.concat(data);
      }
      
    },
    addMovie(state, data){
      if(state.isMovieEdit){
        state.movies[state.index] = data;
        state.isMovieEdit = false;
      }else{
        state.movies = state.movies.concat(data);
      }
      
    },

    showDetails(state, data){
      state.index = data;
      state.showMore = true;
    },
    hideShowDetails(state){
      state.showMore = false;
    },
  
    deleteMovie(state, index){
      state.movies.splice(index, 1);
    },
    editActorModal(state, data){
      state.showActorModal = true;
      state.index = data.index;
      state.isEdit = data.isEdit;
      state.actorDetails = data.actorDetails;
    },
    deleteActor(state, index){
      state.actors.splice(index, 1);
    },
    editProducerModal(state, data){
      state.showProducerModal = true;
      state.index = data.index;
      state.isEdit = data.isEdit;
      state.producerDetails = data.producerDetails;
    },
    deleteProducer(state, index){
      state.producers.splice(index, 1);
    }
 }

 const actions = {
      changeView(context, payload){
         
        context.commit('changeView', payload)
      },

      showMovieModal(context, payload){
        context.commit('showMovieModal', payload);
      },

      hideMovieModal(context){
        context.commit('hideMovieModal');
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
      },

      editMovie(context, data){
        context.commit('editMovie', data);
      },

      deleteMovie(context, index){
        context.commit('deleteMovie', index);
      },

      editActorModal(context, payload){
        context.commit('editActorModal', payload);
      },

      deleteActor(context, index){
        context.commit('deleteActor', index);
      },
      
      editProducerModal(context, payload){
        context.commit('editProducerModal', payload);
      },

      deleteProducer(context, index){
        context.commit('deleteProducer', index);
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
    showFormDetails : state => {
      return state.showFormDetails;
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
    },
    isEdit : state => {
      return state.isEdit;
    },
    isMovieEdit : state => {
      return state.isMovieEdit;
    }
    
 } 


export default new Vuex.Store({
  state,
  mutations,
  actions,
  getters
});
