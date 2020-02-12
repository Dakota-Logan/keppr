<template>
	<div class="keep">
		<div @click="loadKeep(keep.id)">
			<h3>{{keep.name}}</h3>
			<p>{{keep.description}}</p>
			<p>Views: {{keep.views}} - Keeps: {{keep.keeps}} - Shares: {{keep.shares}}</p>
			<img :src="keep.img" alt="No image available."/>
		</div>
		<select v-if="$auth.isAuthenticated" name="vault" id="vaultSelect" v-model="vaultId">
			<option :value="vault.id" v-for="vault in vaults" :key="vault.id">{{vault.name}}</option>
		</select>
		<button v-if="$auth.isAuthenticated" type="button" @click="vault">~Vault It~</button>
		<button v-if="isOwner" type="button" @click="deleteKeep">Delete</button>
		<button v-if="$auth.isAuthenticated" type="button" @click="shareKeep">Share</button>
	</div>
</template>

<script>
	export default {
		name: "Home_keep",
		props: ["keep"],
		data () {
			return {
				vaultId: null
			}
		},
		mounted () {
			console.log (this.keep);
		},
		methods: {
			loadKeep (id) {
				this.$store.dispatch("keepView", this.keep.id);
				this.$router.push ("keep/" + id);
			},
			isOwner () {
				if (this.keep.userId === this.$auth.userInfo.sub)
					return true;
				else
					return false;
			},
			vault () {
				if (this.vaultId === null) return;
				this.$store.dispatch ("vault", {
					userId: this.$auth.userInfo.sub, id: this.keep.id, VaultId: this.vaultId
				});
			},
			deleteKeep () {
				this.$store.dispatch ("deleteKeep", this.keep.id);
			},
			shareKeep() {
				this.$store.dispatch("keepShares", this.keep.id);
			}
		},
		computed: {
			vaults () {
				return this.$store.state.userVaults;
			}
		}
	}
</script>

<style scoped>
	div {
		width: 20vw;
		margin: 1.5vw;
	}
</style>