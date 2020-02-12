<template>
	<div class="dashboard">
		<h1>WELCOME TO THE DASHBOARD</h1>
		vaults
		<Dashboard_Vault_Form/>
		<div v-for="vault in userVaults" @click="getVault(vault.id)">
			<h3>{{vault.name}}</h3>
			<p>{{vault.description}}</p>
			<button type="button" @click="deleteVault(vault.id)">Delete</button>
		</div>
		
		user {{ userKeeps }}
	</div>
</template>

<script>
	import Dashboard_Vault_Form from "../components/Dashboard_Vault_Form";
	
	export default {
		name: "dashboard",
		components: {
			Dashboard_Vault_Form
		},
		mounted () {
			this.$store.dispatch ("getUserKeeps");
		},
		computed: {
			userVaults () {
				return this.$store.state.userVaults;
			},
			userKeeps () {
				return this.$store.state.userKeeps;
			}
		},
		methods: {
			getVault (id) {
				this.$store.dispatch ("getVaultKeeps", id);
				this.$router.push ("vault/" + id);
			},
			deleteVault (id) {
				console.log (id)
				this.$store.dispatch ("deleteVault", id)
			}
		}
	};
</script>

<style scoped>

</style>
