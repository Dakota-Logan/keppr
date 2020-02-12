<template>
	<div class="home">
		<h1>Welcome Home</h1>
		<Home_Keep_Form/>
		<div id="view-keeps">
		<Home_keep v-for="keep in keeps" :key="keep.Id" :keep="keep"/>
		</div>
	</div>
</template>

<script>
	import Home_keep from "../components/Home_keep";
	import Home_Keep_Form from "../components/Home_Keep_Form";
	export default {
		name: "home",
		components:{
			Home_keep,
			Home_Keep_Form
		},
		mounted () {
			this.$store.dispatch("GetKeeps");
		},
		computed: {
			user () {
				return this.$store.state.user;
			},
			keeps () {
				return this.$store.state.publicKeeps;
			}
		},
		methods: {
			logout () {
				this.$store.dispatch ("logout");
			}
		}
	};
</script>
<style scoped>
	#view-keeps{
		display: flex;
		flex-wrap: wrap;
		padding-top: 3vh;
	}
</style>
