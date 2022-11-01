modname = Juggernaut
gamepath = /mnt/c/Program\ Files\ \(x86\)/Steam/steamapps/common/Outward/Outward_Defed
pluginpath = BepInEx/plugins
sideloaderpath = $(pluginpath)/$(modname)/SideLoader

dependencies = CustomWeaponBehaviour SynchronizedWorldObjects TinyHelper

assemble:
	# common for all mods
	rm -f -r public
	mkdir -p public/$(pluginpath)/$(modname)
	cp bin/$(modname).dll public/$(pluginpath)/$(modname)/
	for dependency in $(dependencies) ; do \
		cp ../$${dependency}/bin/$${dependency}.dll public/$(pluginpath)/$(modname)/ ; \
	done
	
	# crusader specific
	mkdir -p public/$(sideloaderpath)/Items
	mkdir -p public/$(sideloaderpath)/Texture2D
	mkdir -p public/$(sideloaderpath)/AssetBundles
	
	mkdir -p public/$(sideloaderpath)/Items/Bastard/Textures
	cp resources/icons/sword_in_rock.png                    public/$(sideloaderpath)/Items/Bastard/Textures/icon.png
	cp resources/icons/sword_in_rock_small.png              public/$(sideloaderpath)/Items/Bastard/Textures/skillicon.png
	mkdir -p public/$(sideloaderpath)/Items/Fortified/Textures
	cp resources/icons/fortified.png                        public/$(sideloaderpath)/Items/Fortified/Textures/icon.png
	cp resources/icons/fortified_small.png                  public/$(sideloaderpath)/Items/Fortified/Textures/skillicon.png
	mkdir -p public/$(sideloaderpath)/Items/HordeBreaker/Textures
	cp resources/icons/horde_breaker.png                    public/$(sideloaderpath)/Items/HordeBreaker/Textures/icon.png
	cp resources/icons/horde_breaker_small.png              public/$(sideloaderpath)/Items/HordeBreaker/Textures/skillicon.png
	mkdir -p public/$(sideloaderpath)/Items/Ruthless/Textures
	cp resources/icons/ruthless.png                         public/$(sideloaderpath)/Items/Ruthless/Textures/icon.png
	cp resources/icons/ruthless_small.png                   public/$(sideloaderpath)/Items/Ruthless/Textures/skillicon.png
	mkdir -p public/$(sideloaderpath)/Items/Tackle/Textures
	cp resources/icons/tackle.png                           public/$(sideloaderpath)/Items/Tackle/Textures/icon.png
	cp resources/icons/tackle_small.png                     public/$(sideloaderpath)/Items/Tackle/Textures/skillicon.png
	mkdir -p public/$(sideloaderpath)/Items/Unyielding/Textures
	cp resources/icons/unyielding.png                       public/$(sideloaderpath)/Items/Unyielding/Textures/icon.png
	cp resources/icons/unyielding_small.png                 public/$(sideloaderpath)/Items/Unyielding/Textures/skillicon.png
	mkdir -p public/$(sideloaderpath)/Items/Vengeful/Textures
	cp resources/icons/vengeful.png                         public/$(sideloaderpath)/Items/Vengeful/Textures/icon.png
	cp resources/icons/vengeful_small.png                   public/$(sideloaderpath)/Items/Vengeful/Textures/skillicon.png
	mkdir -p public/$(sideloaderpath)/Items/Warcry/Textures
	cp resources/icons/warcry.png                           public/$(sideloaderpath)/Items/Warcry/Textures/icon.png
	cp resources/icons/warcry_small.png                     public/$(sideloaderpath)/Items/Warcry/Textures/skillicon.png
	
publish:
	make assemble
	rm -f $(modname).rar
	rar a $(modname).rar -ep1 public/*

install:
	make assemble
	rm -r -f $(gamepath)/$(pluginpath)/$(modname)
	cp -r public/* $(gamepath)
clean:
	rm -f -r public
	rm -f $(modname).rar
	rm -f -r bin
info:
	echo Modname: $(modname)
