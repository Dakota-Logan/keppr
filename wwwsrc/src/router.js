import Vue from "vue";
import Router from "vue-router";
import {authGuard} from "@bcwdev/auth0-vue";
// @ts-ignore
import Home from "./views/Home.vue";
import Dashboard from "./views/Dashboard.vue";
import Keep from "./views/Keep.vue";
import Vault from "./views/Vault.vue";

Vue.use (Router);

export default new Router ({
	routes: [
		{
			path: "/",
			name: "home",
			component: Home
		},
		{
			path: "/keep/:id",
			name: "keep",
			component: Keep
		},
		{
			path: "/vault/:id",
			name: "vault",
			component: Vault
		},
		{
			path: "/dashboard",
			name: "dashboard",
			component: Dashboard,
			beforeEnter: authGuard
		}
	]
});
