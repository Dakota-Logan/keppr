<template>
	<div>
		{{keep}}
	</div>
</template>

<script>
	export default {
		name: "Keep",
		mounted () {
			console.log (this.$route.params.id);
			this.$store.dispatch("getKeepById", this.$route.params.id)
		},
		computed: {
			keep(){
				return this.$store.state.primaryKeep;
			}
		},
		beforeDestroy () {
			this.$store.state.primary = {};
		}
		
	}
</script>

<style scoped>

</style>