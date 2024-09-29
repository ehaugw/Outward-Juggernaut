include Makefile.helpers
modname = Juggernaut
dependencies = CustomGrip DelayedDamage Proficiencies SynchronizedWorldObjects TinyHelper

assemble:
	# common for all mods
	rm -f -r public
	@make dllsinto TARGET=$(modname) --no-print-directory
	
	@make basefolders
	
	@make skill NAME="Stoicism" FILENAME="stoicism"
	@make skill NAME="Cull" FILENAME="cull"
	@make skill NAME="Fortified" FILENAME="fortified"
	@make skill NAME="HordeBreaker" FILENAME="horde_breaker"
	@make skill NAME="Ruthless" FILENAME="ruthless"
	@make skill NAME="Tackle" FILENAME="tackle"
	@make skill NAME="Unyielding" FILENAME="unyielding"
	@make skill NAME="Vengeful" FILENAME="vengeful"
	@make skill NAME="Warcry" FILENAME="warcry"
	
forceinstall:
	make assemble
	rm -r -f $(gamepath)/$(pluginpath)/$(modname)
	cp -u -r public/* $(gamepath)

play:
	(make install && cd .. && make play)
