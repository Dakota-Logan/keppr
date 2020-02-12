<template>
	<div id="app">
		<navbar/>
		<router-view/>
	</div>
</template>

<script>
	import Navbar from "@/components/navbar";
	import {onAuth} from "@bcwdev/auth0-vue";
	
	export default {
		name: "App",
		async beforeCreate () {
			await onAuth ();
			this.$store.dispatch ("setBearer", this.$auth.bearer);
			this.$store.dispatch("getVaults");
		},
		components: {
			Navbar
		}
	};
</script>

<style>
</style>
