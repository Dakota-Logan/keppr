import Vue from "vue";
import Vuex from "vuex";
import Axios from "axios";
import router from "./router";

Vue.use (Vuex);

let baseUrl = location.host.includes ("localhost") ? "https://localhost:5001/" : "/";

let api = Axios.create ({
	baseURL: baseUrl + "api/",
	timeout: 3000,
	withCredentials: true
});

export default new Vuex.Store ({
	state: {
		publicKeeps: [],
		userKeeps: [],
		userVaults: [],
		vaultKeeps: [],
		primaryKeep: {},
		primaryVault: null
	},
	mutations: {
		publicKeeps (state, keps) {
			state.publicKeeps = keps;
		},
		primaryKeep (state, kep) {
			state.primaryKeep = kep;
		},
		vaults (state, data) {
			state.userVaults = data;
		},
		vaultKeeps(state, data){
			state.vaultKeeps = data;
		},
		primaryVault (state, data) {
			state.primaryVault = data;
		}
		
	},
	actions: {
		setBearer ({}, bearer) {
			api.defaults.headers.authorization = bearer;
		},
		resetBearer () {
			api.defaults.headers.authorization = "";
		},
		GetKeeps ({commit}) {
			api.get ('keeps').then (res => {
				commit ("publicKeeps", res.data);
			}).catch (e => console.error (e));
		},
		getVaults ({commit}) {
			setTimeout({}, 150);
			api.get("vaults")
			.then(res=> {commit("vaults", res.data)})
			.catch(e=>console.error(e))
		},
		getKeepById ({commit}, id) {
			api.get ("/keeps/" + id)
			.then (res => {
				commit ("primaryKeep", res.data)
			})
			.catch (e => {
				console.error (e)
			})
		},
		newKeep ({commit}, obj) {
			let newKeep = {
				"Name": obj.name
				, "Description": obj.desc
				, "IsPrivate": obj.tf
				, "Img": obj.img
			};
			api.post ("keeps", newKeep)
			.then (res=>{
				commit("primaryKeep", res.data);
				router.push("keep/"+res.data.id);
			})
			.catch (e => console.error(e))
		},
		newVault ({commit}, obj) {
			let newKeep = {
				"Name": obj.name
				, "Description": obj.desc
			};
			api.post ("vaults", newKeep)
			.catch (e => console.error(e));
		},
		vault ({commit}, data) {
			api.post("vaultkeeps", {
				UserId: data.userId,
				VaultId: data.VaultId,
				KeepId: data.id
			})
			.then(res=>{
				console.log (res.data);
				api.put("keeps/"+data.id+"/keeps");
			})
			.catch(e=>console.error(e))
		},
		deleteKeep ({dispatch}, id) {
			api.delete("keeps/"+id).then(
			dispatch("GetKeeps")
			).catch(e=>console.error(e));
		},
		deleteVault ({commit}, id) {
			api.delete("vaults/"+id).catch(e=>console.error(e));
		},
		getUserKeeps ({commit}){
			api.get("keeps")
			.then(res=>{})
			.catch(e=>console.error(e));
		},
		getVaultKeeps ({commit}, id) {
			api.get("vaultkeeps/"+id+"/keeps")
			.then(res=>{
				commit("vaultKeeps", res.data);
			}).catch(e=>console.error(e));
		},
		getVault ({commit}, id) {
			api.get("vaults/"+id)
			.then(res=> {commit("primaryVault", res.data)})
			.catch(e=>console.error(e));
		},
		keepView ({}, id) {
			api.put("keeps/"+id+"/views").catch (e => console.error(e));
		},
		keepShares ({}, id) {
			api.put("keeps/"+id+"/shares").catch (e => console.error(e));
		}
	}
});
