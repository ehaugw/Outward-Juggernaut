modname = Juggernaut
gamepath = /mnt/c/Program\ Files\ \(x86\)/Steam/steamapps/common/Outward/Outward_Defed
pluginpath = BepInEx/plugins
sideloaderpath = $(pluginpath)/$(modname)/SideLoader

dependencies = HolyDamageManager DelayedDamage CustomWeaponBehaviour SynchronizedWorldObjects TinyHelper

assemble:
	# common for all mods
	rm -f -r public
	mkdir -p public/$(pluginpath)/$(modname)
	cp -u bin/$(modname).dll public/$(pluginpath)/$(modname)/
	for dependency in $(dependencies) ; do \
		cp -u ../$${dependency}/bin/$${dependency}.dll public/$(pluginpath)/$(modname)/ ; \
	done
	
	mkdir -p public/$(sideloaderpath)/Items
	mkdir -p public/$(sideloaderpath)/Texture2D
	mkdir -p public/$(sideloaderpath)/AssetBundles
	
	mkdir -p public/$(sideloaderpath)/Items/Bastard/Textures
	cp -u resources/icons/sword_in_rock.png                    public/$(sideloaderpath)/Items/Bastard/Textures/icon.png
	cp -u resources/icons/sword_in_rock_small.png              public/$(sideloaderpath)/Items/Bastard/Textures/skillicon.png
	mkdir -p public/$(sideloaderpath)/Items/Fortified/Textures
	cp -u resources/icons/fortified.png                        public/$(sideloaderpath)/Items/Fortified/Textures/icon.png
	cp -u resources/icons/fortified_small.png                  public/$(sideloaderpath)/Items/Fortified/Textures/skillicon.png
	mkdir -p public/$(sideloaderpath)/Items/HordeBreaker/Textures
	cp -u resources/icons/horde_breaker.png                    public/$(sideloaderpath)/Items/HordeBreaker/Textures/icon.png
	cp -u resources/icons/horde_breaker_small.png              public/$(sideloaderpath)/Items/HordeBreaker/Textures/skillicon.png
	mkdir -p public/$(sideloaderpath)/Items/Ruthless/Textures
	cp -u resources/icons/ruthless.png                         public/$(sideloaderpath)/Items/Ruthless/Textures/icon.png
	cp -u resources/icons/ruthless_small.png                   public/$(sideloaderpath)/Items/Ruthless/Textures/skillicon.png
	mkdir -p public/$(sideloaderpath)/Items/Tackle/Textures
	cp -u resources/icons/tackle.png                           public/$(sideloaderpath)/Items/Tackle/Textures/icon.png
	cp -u resources/icons/tackle_small.png                     public/$(sideloaderpath)/Items/Tackle/Textures/skillicon.png
	mkdir -p public/$(sideloaderpath)/Items/Unyielding/Textures
	cp -u resources/icons/unyielding.png                       public/$(sideloaderpath)/Items/Unyielding/Textures/icon.png
	cp -u resources/icons/unyielding_small.png                 public/$(sideloaderpath)/Items/Unyielding/Textures/skillicon.png
	mkdir -p public/$(sideloaderpath)/Items/Vengeful/Textures
	cp -u resources/icons/vengeful.png                         public/$(sideloaderpath)/Items/Vengeful/Textures/icon.png
	cp -u resources/icons/vengeful_small.png                   public/$(sideloaderpath)/Items/Vengeful/Textures/skillicon.png
	mkdir -p public/$(sideloaderpath)/Items/Warcry/Textures
	cp -u resources/icons/warcry.png                           public/$(sideloaderpath)/Items/Warcry/Textures/icon.png
	cp -u resources/icons/warcry_small.png                     public/$(sideloaderpath)/Items/Warcry/Textures/skillicon.png
	
publish:
	make clean
	make assemble
	rar a $(modname).rar -ep1 public/*
	
	cp -r public/BepInEx thunderstore
	mv thunderstore/plugins/$(modname)/* thunderstore/plugins
	rmdir thunderstore/plugins/$(modname)
	
	(cd ../Descriptions && python3 $(modname).py)
	
	cp -u resources/manifest.json thunderstore/
	cp -u README.md thunderstore/
	cp -u resources/icon.png thunderstore/
	(cd thunderstore && zip -r $(modname)_thunderstore.zip *)
	cp -u ../tcli/thunderstore.toml thunderstore
	(cd thunderstore && tcli publish --file $(modname)_thunderstore.zip) || true
	mv thunderstore/$(modname)_thunderstore.zip .

install:
	make assemble
	rm -r -f $(gamepath)/$(pluginpath)/$(modname)
	cp -u -r public/* $(gamepath)
clean:
	rm -f -r public
	rm -f -r thunderstore
	rm -f $(modname).rar
	rm -f $(modname)_thunderstore.zip
	rm -f resources/manifest.json
	rm -f README.md
info:
	echo Modname: $(modname)
play:
	(make install && cd .. && make play)
edit:
	nvim ../Descriptions/$(modname).py
readme:
	(cd ../Descriptions/ && python3 $(modname).py)
