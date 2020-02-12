<template>
	<div>
		<h1>{{vault.name}}</h1>
		<h3>{{vault.description}}</h3>
		<Home_keep v-for="keep in keeps" :keep="keep" :key="keep.id"/>
	</div>
</template>

<script>
	import Home_keep from "../components/Home_keep";
	
	export default {
		name: "vault",
		components: {
			Home_keep
		},
		mounted () {
				// this.$router.push("home");
			this.$store.dispatch ("getVault", this.$route.params.id);
			this.$store.dispatch ("getVaultKeeps", this.$route.params.id);
			setTimeout(()=>{if(this.$store.state.primaryVault==null)
				this.$router.push("/")}, 300)
		},
		computed: {
			keeps () {
				return this.$store.state.vaultKeeps;
			},
			vault () {
				if(this.$store.state.primaryVault == null)
					return {name:'', description: ''}
				return this.$store.state.primaryVault;
			}
		},
		methods: {}
	}
</script>

<style scoped>

</style>